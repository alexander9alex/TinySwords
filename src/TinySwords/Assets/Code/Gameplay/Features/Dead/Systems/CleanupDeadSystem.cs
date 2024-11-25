using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Dead.Systems
{
  public class CleanupDeadSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(16);

    public CleanupDeadSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.Dead));
    }

    public void Cleanup()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.isDestructed = true;
      }
    }
  }
}
