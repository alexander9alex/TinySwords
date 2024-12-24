using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Battle.Services;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Common.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Services
{
  public class RecruitUnitService : IRecruitUnitService
  {
    private const int BaseLayer = 0;

    private readonly IStaticDataService _staticData;
    private readonly IAttackAnimationService _attackAnimationService;
    private readonly IDelayService _delayService;

    public RecruitUnitService(IStaticDataService staticData, IAttackAnimationService attackAnimationService, IDelayService delayService)
    {
      _staticData = staticData;
      _attackAnimationService = attackAnimationService;
      _delayService = delayService;
    }

    public void RecruitUnit(GameEntity unit)
    {
      unit
        .ReplaceTeamColor(TeamColor.Blue)
        .ReplaceAllUnitCommandTypeIds(new()
        {
          CommandTypeId.Move,
          CommandTypeId.MoveWithAttack,
          CommandTypeId.AimedAttack,
        })
        .With(x => x.isSelectable = true)
        .With(x => x.isUnselected = true);

      ChangeAnimator(unit);
    }

    private void ChangeAnimator(GameEntity unit)
    {
      Animator animator = GetUnitAnimator(unit);

      float animationPlaybackTime = AnimationPlaybackTime(animator);
      int animationNameHash = AnimationNameHash(animator);
      AnimationEvent[] animationEvents = AnimationEvents(animator);

      ChangeRuntimeAnimatorController(animator, unit.UnitTypeId);
      ContinueAnimation(animator, animationNameHash, animationPlaybackTime);

      foreach (AnimationEvent animationEvent in animationEvents)
        ProcessAnimationEvent(animationEvent, animationPlaybackTime, unit.Id);
    }

    private void ProcessAnimationEvent(AnimationEvent animationEvent, float animationPlaybackTime, int unitId)
    {
      switch (animationEvent.functionName)
      {
        case nameof(_attackAnimationService.UnitMakeHit):
          ProcessMakeHitEvent(animationEvent.time, animationPlaybackTime, unitId);
          break;
        case nameof(_attackAnimationService.UnitFinishedAttack):
          ProcessFinishedAttack(animationEvent.time, animationPlaybackTime, unitId);
          break;
      }
    }

    private void ProcessMakeHitEvent(float eventTime, float animationPlaybackTime, int unitId)
    {
      if (animationPlaybackTime < eventTime)
        _delayService.MakeActionWithDelay(() => _attackAnimationService.UnitMakeHit(unitId), delay: eventTime - animationPlaybackTime);
    }

    private void ProcessFinishedAttack(float eventTime, float animationPlaybackTime, int unitId)
    {
      if (animationPlaybackTime < eventTime)
        _delayService.MakeActionWithDelay(() => _attackAnimationService.UnitFinishedAttack(unitId), eventTime - animationPlaybackTime);
    }

    private static void ContinueAnimation(Animator animator, int animationNameHash, float animationPlaybackTime) =>
      animator.PlayInFixedTime(animationNameHash, BaseLayer, animationPlaybackTime);

    private void ChangeRuntimeAnimatorController(Animator animator, UnitTypeId unitTypeId) =>
      animator.runtimeAnimatorController = GetUserTeamUnitRuntimeAnimatorController(unitTypeId);

    private static AnimationEvent[] AnimationEvents(Animator animator) =>
      animator.GetCurrentAnimatorClipInfo(0)[0].clip.events;

    private static int AnimationNameHash(Animator animator) =>
      animator.GetCurrentAnimatorStateInfo(0).shortNameHash;

    private RuntimeAnimatorController GetUserTeamUnitRuntimeAnimatorController(UnitTypeId unitTypeId) =>
      _staticData.GetUnitConfig(unitTypeId, GameConstants.UserTeamColor).UnitPrefab.GetComponentInChildren<Animator>().runtimeAnimatorController;

    private static float AnimationPlaybackTime(Animator animator) =>
      animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

    private static Animator GetUnitAnimator(GameEntity unit) =>
      unit.View.gameObject.GetComponentInChildren<Animator>();
  }
}
