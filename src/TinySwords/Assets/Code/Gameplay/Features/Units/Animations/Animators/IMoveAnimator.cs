using UnityEngine;

namespace Code.Gameplay.Features.Units.Animations.Animators
{
  public interface IMoveAnimator
  {
    void AnimateIdle();
    void AnimateWalk(Vector2 dir);
  }
}