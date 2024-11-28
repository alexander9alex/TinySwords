using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Focus.Systems
{
  public class MarkFocusedSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;
    private readonly IGroup<GameEntity> _focusRequests;

    public MarkFocusedSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;
      _focusRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FocusRequest, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _focusRequests)
      {
        List<GameEntity> focusingEntities = GetFocusingEntitiesFromPosition(request.PositionOnScreen);
        
        if (focusingEntities.Count > 0)
          FocusEntity(focusingEntities.First());

        request.isDestructed = true;
      }
    }

    private static void FocusEntity(GameEntity entity)
    {
      entity.isUnfocused = false;
      entity.isFocused = true;
    }

    private List<GameEntity> GetFocusingEntitiesFromPosition(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
        _cameraProvider.MainCamera.ScreenToWorldPoint(mousePos),
        GameConstants.FocusRadius,
        GameConstants.FocusLayerMask)
        .Where(x => x.isFocusing)
        .Where(x => x.hasTransform)
        .OrderBy(entity => entity.Transform.position.y)
        .ToList();
    }
  }
}
