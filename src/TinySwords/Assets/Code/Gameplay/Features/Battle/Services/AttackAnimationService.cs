using System;
using System.Collections;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Infrastructure.Common.CoroutineRunner;
using UnityEngine;

namespace Code.Gameplay.Features.Battle.Services
{
  public class AttackAnimationService : IAttackAnimationService
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
