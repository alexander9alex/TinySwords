using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Destruct.Systems
{
  public class CleanupDestructedViewSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public CleanupDestructedViewSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Destructed, 
          GameMatcher.View));
    }

    public void Cleanup()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.View.ReleaseEntity();
        Object.Destroy(entity.View.gameObject);
      }
    }
  }
}
