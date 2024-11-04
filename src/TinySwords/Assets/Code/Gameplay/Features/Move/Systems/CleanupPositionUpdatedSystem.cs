using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class CleanupPositionUpdatedSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _positionUpdatedEntities;
    private readonly List<GameEntity> _buffer = new(64);

    public CleanupPositionUpdatedSystem(GameContext game)
    {
      _positionUpdatedEntities = game.GetGroup(GameMatcher.AllOf(GameMatcher.PositionUpdated));
    }

    public void Cleanup()
    {
      foreach (GameEntity updated in _positionUpdatedEntities.GetEntities(_buffer))
      {
        updated.isPositionUpdated = false;
      }
    }
  }
}
