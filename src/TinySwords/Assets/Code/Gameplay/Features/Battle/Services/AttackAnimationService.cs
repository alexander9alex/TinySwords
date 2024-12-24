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
    private readonly ICoroutineRunner _coroutineRunner;

    public AttackAnimationService(ICoroutineRunner coroutineRunner) =>
      _coroutineRunner = coroutineRunner;

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

    public void UnitMakeHitWithDelay(int unitId, float delay) =>
      _coroutineRunner.StartCoroutine(MakeActionWithDelay(() => UnitMakeHit(unitId), delay));

    public void UnitFinishedAttack(int unitId, float delay) =>
      _coroutineRunner.StartCoroutine(MakeActionWithDelay(() => UnitFinishedAttack(unitId), delay));

    private IEnumerator MakeActionWithDelay(Action action, float delay)
    {
      yield return new WaitForSeconds(delay);
      action?.Invoke();
    }
  }
}
