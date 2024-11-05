using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interact.Systems
{
  public class PickEntitiesForInteractionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _interactionRequests;
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    public PickEntitiesForInteractionSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _interactionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.InteractionRequest, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _interactionRequests)
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
