using Code.Common.Entities;
using Code.Common.Extensions;

namespace Code.Gameplay.Features.Battle.Services
{
  class AttackAnimationService : IAttackAnimationService
  {
    public void UnitMakeHit(int unitId)
    {
      CreateEntity.Empty()
        .AddCasterId(unitId)
        .With(x => x.isMakeHit = true);
    }

    public void UnitFinishedAttack(int unitId)
    {
      CreateEntity.Empty()
        .AddCasterId(unitId)
        .With(x => x.isFinishAttack = true);
    }
  }
}
