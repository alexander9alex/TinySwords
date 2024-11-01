using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class TurnOnNavMeshAgentAfterTransformUpdatingSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(64);

    public TurnOnNavMeshAgentAfterTransformUpdatingSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.NavMeshAgent, GameMatcher.PositionUpdated));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.NavMeshAgent.enabled = true;
      }
    }
  }
}
