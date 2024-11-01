using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class UpdateMoveDirectionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public UpdateMoveDirectionSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.NavMeshAgent, GameMatcher.Move));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.ReplaceMoveDirection(entity.NavMeshAgent.velocity);
      }
    }
  }

  public class UpdateWorldPositionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entity;

    public UpdateWorldPositionSystem(GameContext game)
    {
      _entity = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.WorldPosition, GameMatcher.Transform)
        .NoneOf(GameMatcher.UpdatePositionAfterSpawning));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entity)
      {
        entity.ReplaceWorldPosition(entity.Transform.position);
      }
    }
  }

}
