using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.FastInteraction.Systems
{
  public class CleanupFastInteractionInputSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _fastInteractionRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupFastInteractionInputSystem(GameContext game)
    {
      _fastInteractionRequests = game.GetGroup(GameMatcher.FastInteraction);
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _fastInteractionRequests.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
