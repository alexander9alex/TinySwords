using System.Collections.Generic;
using Entitas;

namespace Code.Infrastructure.Views.Systems
{
  public class InitViewPositionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(64);

    public InitViewPositionSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.InitializationRequest, GameMatcher.Transform, GameMatcher.WorldPosition));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.Transform.position = entity.WorldPosition;
        entity.isTurnOnNavMeshAgent = true;
        entity.isInitialized = true;
      }
    }
  }
}
