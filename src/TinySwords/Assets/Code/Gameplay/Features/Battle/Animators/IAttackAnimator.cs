using UnityEngine;

namespace Code.Gameplay.Features.Battle.Animators
{
  public interface IAttackAnimator
  {
    void AnimateAttack(Vector2 dir);
    void InitializeAttackAnimator(int unitId);
  }
}
