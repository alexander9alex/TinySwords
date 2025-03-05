using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interactions.Select.Systems
{
  public class SelectHighlightedUnitsSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _interactions;

    public SelectHighlightedUnitsSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _interactions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Interaction,
          GameMatcher.StartPosition,
          GameMatcher.EndPosition,
          GameMatcher.CompleteInteraction
        ));

      _highlights = game.GetGroup(GameMatcher.Highlight);
    }

    public void Execute()
    {
      foreach (GameEntity interaction in _interactions)
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity entity in GetHighlightedSelectables(highlight.CenterPosition, interaction.StartPosition, interaction.EndPosition))
      {
        if (entity.isSelectable)
          Select(entity);

        CreateEntity.Empty()
          .With(x => x.isUnselectPreviouslySelected = true);

        CreateEntity.Empty()
          .With(x => x.isUpdateHudControlButtons = true);
      }
    }

    private static void Select(GameEntity entity)
    {
      entity.isUnselected = false;
      entity.isSelected = true;
      entity.isSelectedNow = true;
    }

    private IEnumerable<GameEntity> GetHighlightedSelectables(Vector2 centerPos, Vector2 startPos, Vector2 endPos)
    {
      return _physicsService.BoxCast(
        HighlightPos(centerPos),
        HighlightSize(startPos, endPos),
        GameConstants.SelectionLayerMask);
    }

    private Vector3 HighlightPos(Vector2 centerPos) =>
      _cameraProvider.ScreenToWorldPoint(centerPos);

    private Vector3 HighlightSize(Vector2 startPos, Vector2 endPos)
    {
      Vector2 min = Vector2.Min(startPos, endPos);
      Vector2 max = Vector2.Max(startPos, endPos);

      return _cameraProvider.ScreenToWorldPoint(max) - _cameraProvider.ScreenToWorldPoint(min);
    }
  }
}
