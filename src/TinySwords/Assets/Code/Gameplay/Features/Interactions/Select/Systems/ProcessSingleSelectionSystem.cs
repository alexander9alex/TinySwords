using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interactions.Select.Systems
{
  public class ProcessSingleSelectionSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _interactions;

    public ProcessSingleSelectionSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _interactions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Interaction,
          GameMatcher.StartPosition,
          GameMatcher.EndPosition
        ).NoneOf(GameMatcher.Processed));
    }

    public void Execute()
    {
      foreach (GameEntity interaction in _interactions)
      {
        List<GameEntity> selectableEntities = GetSelectableEntitiesFromPosition(interaction.EndPosition);

        if (selectableEntities.Count > 0)
        {
          SelectEntity(selectableEntities.First());

          CreateEntity.Empty()
            .With(x => x.isUnselectPreviouslySelected = true);

          CreateEntity.Empty()
            .With(x => x.isUpdateHudControlButtons = true);
        }
      }
    }

    private static void SelectEntity(GameEntity entity)
    {
      entity.isUnselected = false;
      entity.isSelected = true;
      entity.isSelectedNow = true;
    }

    private List<GameEntity> GetSelectableEntitiesFromPosition(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.ScreenToWorldPoint(mousePos),
          GameConstants.ClickRadius, GameConstants.SelectionLayerMask)
        .Where(entity => entity.isSelectable)
        .Where(entity => entity.hasTransform)
        .OrderBy(entity => entity.Transform.position.y)
        .ToList();
    }
  }
}
