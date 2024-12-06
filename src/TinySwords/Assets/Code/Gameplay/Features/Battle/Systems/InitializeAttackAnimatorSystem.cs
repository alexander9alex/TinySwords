using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class InitializeAttackAnimatorSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _attackAnimator;

    public InitializeAttackAnimatorSystem(GameContext game)
    {
      _attackAnimator = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.CanAttack, GameMatcher.InitializationRequest, GameMatcher.AttackAnimator, GameMatcher.Id));
    }

    public void Execute()
    {
      foreach (GameEntity animator in _attackAnimator)
      {
        animator.AttackAnimator.InitializeAttackAnimator(animator.Id);
      }
    }
  }
}
