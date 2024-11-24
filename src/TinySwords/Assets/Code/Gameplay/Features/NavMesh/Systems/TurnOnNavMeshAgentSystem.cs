using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.NavMesh.Systems
{
  public class TurnOnNavMeshAgentSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(64);

    public TurnOnNavMeshAgentSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.NavMeshAgent, GameMatcher.TurnOnNavMeshAgent));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.NavMeshAgent.enabled = true;

        entity.isTurnOnNavMeshAgent = false;
      }
    }
  }
}
