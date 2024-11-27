using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class CleanupApplyCommandRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _applyControlActionRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupApplyCommandRequestSystem(GameContext game)
    {
      _applyControlActionRequests = game.GetGroup(GameMatcher.ApplyCommand);
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _applyControlActionRequests.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
