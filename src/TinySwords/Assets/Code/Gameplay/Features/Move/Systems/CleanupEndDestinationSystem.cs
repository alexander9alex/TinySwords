using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Systems
{
  public class CleanupEndDestinationSystem : ICleanupSystem
  {
    private const float StoppingDistance = 0.01f;
    
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(64);
    public CleanupEndDestinationSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.NavMeshAgent, GameMatcher.WorldPosition, GameMatcher.EndDestination));
    }

    public void Cleanup()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        if (Vector2.Distance(entity.WorldPosition, entity.EndDestination) <= StoppingDistance)
        {
          entity.RemoveEndDestination();
          
          if (entity.hasCommandTypeId)
            entity.RemoveCommandTypeId();
        }
      }
    }
  }
}
