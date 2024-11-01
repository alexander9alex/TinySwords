using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Destruct.Systems
{
  public class CleanupDestructedSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public CleanupDestructedSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher.Destructed);
    }

    public void Cleanup()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.Destroy();
      }
    }
  }
}
