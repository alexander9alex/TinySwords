using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Death.Systems
{
  public class CleanupDeadEntitiesSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _deadEntities;
    private readonly List<GameEntity> _buffer = new(16);

    public CleanupDeadEntitiesSystem(GameContext game)
    {
      _deadEntities = game.GetGroup(GameMatcher.AllOf(GameMatcher.Dead));
    }

    public void Cleanup()
    {
      foreach (GameEntity entity in _deadEntities.GetEntities(_buffer))
      {
        entity.isDestructed = true;
      }
    }
  }
}
