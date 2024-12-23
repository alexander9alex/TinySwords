using System.Collections.Generic;
using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class MoveToDestinationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _movables;

    public MoveToDestinationSystem(GameContext game)
    {
      _movables = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.NavMeshAgent,
          GameMatcher.Destination,
          GameMatcher.NotAttacking,
          GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity movable in _movables)
      {
        movable.NavMeshAgent.SetDestination(movable.Destination);
      }
    }
  }

  public class CalculateSpeedSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    
    private readonly IGroup<GameEntity> _movables;
    private readonly List<GameEntity> _buffer = new(128);

    public CalculateSpeedSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _movables = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.NavMeshAgent,
          GameMatcher.Speed,
          GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity movable in _movables.GetEntities(_buffer))
      {
        movable.NavMeshAgent.speed = movable.Speed * _time.TimeScale;
      }
    }
  }
}
