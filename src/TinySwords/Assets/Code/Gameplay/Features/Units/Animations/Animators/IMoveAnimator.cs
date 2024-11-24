using UnityEngine;

namespace Code.Gameplay.Features.Units.Animations.Animators
{
  public interface IMoveAnimator
  {
    Vector2 LookDirection { get; }
    void AnimateIdle();
    void AnimateWalk(Vector2 dir);
  }
}