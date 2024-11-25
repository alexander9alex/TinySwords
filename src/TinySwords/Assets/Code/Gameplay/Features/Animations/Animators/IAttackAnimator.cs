using UnityEngine;

namespace Code.Gameplay.Features.Animations.Animators
{
  public interface IAttackAnimator
  {
    void AnimateAttack(Vector2 dir);
    void InitializeAttackAnimator(int unitId);
  }
}
