using System.Collections.Generic;
using Code.Gameplay.Services;
using Entitas;

namespace Code.Infrastructure.Views.Systems
{
  public class InitSpeedSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(64);

    public InitSpeedSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.InitializationRequest, GameMatcher.Speed, GameMatcher.NavMeshAgent));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.NavMeshAgent.speed = entity.Speed * _time.TimeScale;
      }
    }
  }
}
