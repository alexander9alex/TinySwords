using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class CleanupProcessCommandRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _processCommandRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupProcessCommandRequestSystem(GameContext game)
    {
      _processCommandRequests = game.GetGroup(GameMatcher.ProcessCommandRequest);
    }

    public void Cleanup()
    {
      foreach (GameEntity command in _processCommandRequests.GetEntities(_buffer))
      {
        command.isDestructed = true;
      }
    }
  }
}
