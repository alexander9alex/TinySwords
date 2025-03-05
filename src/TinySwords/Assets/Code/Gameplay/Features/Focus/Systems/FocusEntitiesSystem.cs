using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Focus.Systems
{
  public class FocusEntitiesSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;
    private readonly IGroup<GameEntity> _inputs;

    public FocusEntitiesSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.MousePosition
        ));
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs)
      {
        List<GameEntity> focusingEntities = GetFocusingEntitiesFromPosition(input.MousePosition);

        if (focusingEntities.Count > 0)
          FocusEntity(focusingEntities.First());
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
          _cameraProvider.ScreenToWorldPoint(mousePos),
          GameConstants.FocusRadius,
          GameConstants.FocusLayerMask)
        .Where(x => x.isFocusing)
        .Where(x => x.hasTransform)
        .OrderBy(entity => entity.Transform.position.y)
        .ToList();
    }
  }
}
