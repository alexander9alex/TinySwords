using Code.Gameplay.Features.Units.Animations.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Animations.Animators
{
  public class TorchGoblinAnimator : MonoBehaviour, IMoveAnimator, IAttackAnimator
  {
    private const float FlipXMinValue = 0.1f;

    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public Vector2 LookDirection { get; private set; }

    public void AnimateIdle() =>
      Animator.Play(AnimationConstants.Idle);

    public void AnimateWalk(Vector2 dir)
    {
      TurnToDirX(dir);
      Animator.Play(AnimationConstants.Walk);
      LookDirection = dir.normalized;
    }
    

    public void AnimateAttack(Vector2 dir)
    {
      SpriteRenderer.flipX = false;

      if (TargetOnY(dir))
        AnimateAttackByY(dir);
      else
        AnimateAttackByX(dir);

      LookDirection = dir.normalized;
    }
    
    private static bool TargetOnY(Vector2 dir)
    {
      return Mathf.Abs(dir.y) > Mathf.Abs(dir.x);
    }

    private void AnimateAttackByX(Vector2 dir)
    {
      TurnToDirX(dir);
      Animator.Play(AnimationConstants.AttackRight);
    }

    private void AnimateAttackByY(Vector2 dir) =>
      Animator.Play(dir.y > 0 ? AnimationConstants.AttackTop : AnimationConstants.AttackBottom);

    private void TurnToDirX(Vector2 dir)
    {
      if (Mathf.Abs(dir.x) > FlipXMinValue)
        SpriteRenderer.flipX = dir.x < 0;
    }
  }
}
