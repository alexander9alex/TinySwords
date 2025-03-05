using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.FastInteractions.Systems
{
  public class CleanupFastInteractionSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _fastInteractions;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupFastInteractionSystem(GameContext game)
    {
      _fastInteractions = game.GetGroup(GameMatcher.FastInteraction);
    }

    public void Cleanup()
    {
      foreach (GameEntity fastInteraction in _fastInteractions.GetEntities(_buffer))
      {
        fastInteraction.isDestructed = true;
      }
    }
  }
}
