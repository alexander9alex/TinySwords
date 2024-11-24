using UnityEngine;

namespace Code.Gameplay.Features.Units.Animations.Animators
{
  public interface IAttackAnimator
  {
    void AnimateAttack(Vector2 dir);
  }
}
