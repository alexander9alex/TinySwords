using UnityEngine;

namespace Code.Gameplay.Features.Units.Animations.Services
{
  class AttackAnimationService : IAttackAnimationService
  {
    public void UnitMakeHit(int unitId)
    {
      Debug.Log($"Unit with id {unitId} make hit!");
    }

    public void UnitFinishedAttack(int unitId)
    {
      Debug.Log($"Unit with id {unitId} finished attack...");
    }
  }
}
