using Code.Gameplay.Features.Units.Animations.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Animations.Animators
{
  public class KnightAnimator : MonoBehaviour, ISelectingAnimator, IMoveAnimator
  {
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public GameObject SelectingCircle;

    public void AnimateIdle() =>
      Animator.Play(KnightAnimations.Idle);

    public void AnimateWalk(Vector2 dir)
    {
      SpriteRenderer.flipX = dir.x < 0;
      Animator.Play(KnightAnimations.Walk);
    }

    public void AnimateAttack(Vector2 dir)
    {
      SpriteRenderer.flipX = false;
      
    }

    public void AnimateSelecting() =>
      SelectingCircle.SetActive(true);

    public void AnimateUnselecting() =>
      SelectingCircle.SetActive(false);
  }
}
