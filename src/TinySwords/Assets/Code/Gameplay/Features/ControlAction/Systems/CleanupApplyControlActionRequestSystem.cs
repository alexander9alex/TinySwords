using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.ControlAction.Systems
{
  public class CleanupApplyControlActionRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _applyControlActionRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupApplyControlActionRequestSystem(GameContext game)
    {
      _applyControlActionRequests = game.GetGroup(GameMatcher.ApplyControlAction);
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
