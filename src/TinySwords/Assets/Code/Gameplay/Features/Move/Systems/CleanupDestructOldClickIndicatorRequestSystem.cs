using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class CleanupDestructOldClickIndicatorRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _destructOldMoveIndicatorRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupDestructOldClickIndicatorRequestSystem(GameContext game)
    {
      _destructOldMoveIndicatorRequests = game.GetGroup(GameMatcher.DestructOldMoveIndicatorRequest);
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _destructOldMoveIndicatorRequests.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
