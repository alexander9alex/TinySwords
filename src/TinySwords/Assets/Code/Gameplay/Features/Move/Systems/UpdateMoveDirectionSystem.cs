using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Systems
{
  public class UpdateMoveDirectionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public UpdateMoveDirectionSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.NavMeshAgent, GameMatcher.MoveDirection));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.ReplaceMoveDirection(entity.NavMeshAgent.velocity.normalized);
      }
    }
  }
}
