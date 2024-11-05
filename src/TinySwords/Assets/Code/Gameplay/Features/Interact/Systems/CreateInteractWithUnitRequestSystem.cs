using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Interact.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Interact.Systems
{
  public class CreateInteractWithUnitRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _interactionRequests;
    private readonly List<GameEntity> _buffer = new(1);
    public CreateInteractWithUnitRequestSystem(GameContext game)
    {
      _interactionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.InteractionRequest, GameMatcher.PickedForInteraction)
        .NoneOf(GameMatcher.Processed));
    }

    public void Execute()
    {
      foreach (GameEntity request in _interactionRequests.GetEntities(_buffer))
      foreach (int entityId in request.PickedForInteraction)
      {
        GameEntity entity = entityId.GetEntity();

        if (entity.isUnit)
        {
          CreateEntity.Empty()
            .AddTargetId(entityId)
            .With(x => x.isInteractWithUnitRequest = true);
          
          request.isProcessed = true;
          break;
        }
      }
    }
  }
}
