using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class MoveToDestinationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _movables;

    public MoveToDestinationSystem(GameContext game)
    {
      _movables = game.GetGroup(GameMatcher.AllOf(GameMatcher.NavMeshAgent, GameMatcher.Destination));
    }

    public void Execute()
    {
      foreach (GameEntity movable in _movables)
      {
        movable.NavMeshAgent.SetDestination(movable.Destination);
      }
    }
  }
}
