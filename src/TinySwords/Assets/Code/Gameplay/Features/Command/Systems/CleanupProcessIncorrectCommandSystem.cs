using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class CleanupProcessIncorrectCommandSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _processIncorrectCommandRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupProcessIncorrectCommandSystem(GameContext game)
    {
      _processIncorrectCommandRequests = game.GetGroup(GameMatcher.ProcessIncorrectCommandRequest);
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _processIncorrectCommandRequests.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
