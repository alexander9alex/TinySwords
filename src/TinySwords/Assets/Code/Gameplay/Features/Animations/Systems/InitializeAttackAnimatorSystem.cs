using Entitas;

namespace Code.Gameplay.Features.Animations.Systems
{
  public class InitializeAttackAnimatorSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _attackAnimator;

    public InitializeAttackAnimatorSystem(GameContext game)
    {
      _attackAnimator = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AttackAnimator, GameMatcher.InitializationRequest, GameMatcher.Unit, GameMatcher.Id));
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
