using System.Collections.Generic;
using Entitas;

namespace Code.Infrastructure.Views.Systems
{
  public class InitUnitSpeedSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(64);

    public InitUnitSpeedSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.InitializationRequest, GameMatcher.Unit, GameMatcher.Speed, GameMatcher.NavMeshAgent));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.NavMeshAgent.speed = entity.Speed;
      }
    }
  }
}
