using Code.Gameplay.Features.Units.Animations.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Animations.Animators
{
  public class TorchGoblinAnimator : MonoBehaviour, IMoveAnimator
  {
    private const float FlipXMinValue = 0.1f;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;

    public void AnimateIdle() =>
      Animator.Play(AnimationConstants.Idle);

    public void AnimateWalk(Vector2 dir)
    {
      TurnToMoveDir(dir);
      Animator.Play(AnimationConstants.Walk);
    }

    private void TurnToMoveDir(Vector2 dir)
    {
      if (Mathf.Abs(dir.x) > FlipXMinValue)
        SpriteRenderer.flipX = dir.x < 0;
    }

    public void AnimateAttack(Vector2 dir)
    {
      SpriteRenderer.flipX = false;
      
    }
  }
}
