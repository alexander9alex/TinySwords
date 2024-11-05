using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Select.Systems
{
  public class ProcessSingleSelectionSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _singleSelectionRequests;
    private readonly IGroup<GameEntity> _mousePositions;
    private readonly List<GameEntity> _buffer = new(1);
    
    public ProcessSingleSelectionSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _singleSelectionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.SingleSelectionRequest)
        .NoneOf(GameMatcher.Processed));

      _mousePositions = game.GetGroup(GameMatcher.MousePositionOnScreen);
    }

    public void Execute()
    {
      foreach (GameEntity request in _singleSelectionRequests.GetEntities(_buffer))
      foreach (GameEntity mousePosition in _mousePositions)
      {
        List<GameEntity> selectableEntities = GetSelectableEntitiesFromPosition(mousePosition.MousePositionOnScreen);

        if (selectableEntities.Count > 0)
        {
          SelectEntity(selectableEntities.First());

          request.isProcessed = true;
        }
      }
    }

    private static void SelectEntity(GameEntity entity)
    {
      entity.isUnselected = false;
      entity.isSelected = true;
      entity.isSelectedNow = true;

      CreateEntity.Empty()
        .With(x => x.isUnselectPreviouslySelectedRequest = true);
    }

    private List<GameEntity> GetSelectableEntitiesFromPosition(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.MainCamera.ScreenToWorldPoint(mousePos),
          GameConstants.ClickRadius, GameConstants.SelectionLayerMask)
        .Where(entity => entity.isSelectable)
        .Where(entity => entity.hasTransform)
        .OrderBy(entity => entity.Transform.position.y)
        .ToList();
    }
  }
}
