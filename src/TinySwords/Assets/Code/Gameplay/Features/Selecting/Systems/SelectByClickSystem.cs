using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Selecting.Systems
{
  public class SelectByClickSystem : IExecuteSystem
  {
    private const float ClickRadius = 0.25f;

    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;
    private readonly IGroup<GameEntity> _clicks;
    private readonly IGroup<GameEntity> _mousePositions;
    private readonly List<GameEntity> _buffer = new(1);

    private readonly int _layerMask = 1 << LayerMask.NameToLayer("Unit");

    public SelectByClickSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _clicks = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.LeftClick)
        .NoneOf(GameMatcher.Processed));
      
      _mousePositions = game.GetGroup(GameMatcher.MousePosition);
    }

    public void Execute()
    {
      foreach (GameEntity click in _clicks.GetEntities(_buffer))
      foreach (GameEntity mousePosition in _mousePositions)
      {
        GameEntity entity = GetSelectableEntityFromPosition(mousePosition.MousePosition);

        if (entity != null && entity.isSelectable)
        {
          entity.isUnselected = false;
          entity.isSelected = true;
          entity.isSelectedNow = true;
          
          click.isProcessed = true;

          CreateEntity.Empty()
            .With(x => x.isUnselectPreviouslySelectedRequest = true);
        }
      }
    }

    private GameEntity GetSelectableEntityFromPosition(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.MainCamera.ScreenToWorldPoint(mousePos),
          ClickRadius,
          _layerMask)
        .FirstOrDefault();
    }
  }
}
