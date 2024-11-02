using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class UpdateIdleAvoidanceSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _idles;

    public UpdateIdleAvoidanceSystem(GameContext game)
    {
      _idles = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Idle, GameMatcher.CurrentAvoidancePriority, GameMatcher.IdleAvoidancePriority));
    }

    public void Execute()
    {
      foreach (GameEntity idle in _idles)
      {
        idle.ReplaceCurrentAvoidancePriority(idle.IdleAvoidancePriority);
      }
    }
  }
}
