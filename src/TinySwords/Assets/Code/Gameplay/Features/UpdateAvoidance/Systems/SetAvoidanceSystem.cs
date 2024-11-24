using Entitas;

namespace Code.Gameplay.Features.UpdateAvoidance.Systems
{
  public class SetAvoidanceSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;

    public SetAvoidanceSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.NavMeshAgent, GameMatcher.CurrentAvoidancePriority));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.NavMeshAgent.avoidancePriority = entity.CurrentAvoidancePriority;
      }
    }
  }
}
