using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupFastInteractionSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _interactionRequests;
    private readonly List<GameEntity> _clicksBuffer = new(1);

    public CleanupFastInteractionSystem(GameContext game)
    {
      _interactionRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.FastInteraction));
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
