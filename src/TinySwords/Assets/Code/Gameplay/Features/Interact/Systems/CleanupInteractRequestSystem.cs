using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Interact.Systems
{
  public class CleanupInteractRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _interactionRequests;
    private readonly List<GameEntity> _clicksBuffer = new(1);

    public CleanupInteractRequestSystem(GameContext game)
    {
      _interactionRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.InteractionRequest));
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _interactionRequests.GetEntities(_clicksBuffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
