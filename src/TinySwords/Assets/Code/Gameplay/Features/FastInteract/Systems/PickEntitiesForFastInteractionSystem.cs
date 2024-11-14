using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.FastInteract.Systems
{
  public class PickEntitiesForFastInteractionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _fastInteractions;
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    public PickEntitiesForFastInteractionSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _fastInteractions = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FastInteraction, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _fastInteractions)
      {
        List<GameEntity> interactableEntities = GetInteractableEntityFromPosition(request.PositionOnScreen);

        request.AddPickedForInteraction(new List<int>());
        
        foreach (GameEntity entity in interactableEntities)
        {
          if (entity.hasId)
            request.PickedForInteraction.Add(entity.Id);
        }
      }
    }

    private List<GameEntity> GetInteractableEntityFromPosition(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.MainCamera.ScreenToWorldPoint(mousePos),
          GameConstants.ClickRadius, GameConstants.InteractionLayerMask)
        .Where(entity => entity.isInteractable)
        .Where(entity => entity.hasTransform)
        .OrderBy(entity => entity.Transform.position.y)
        .ToList();
    }
  }
}
