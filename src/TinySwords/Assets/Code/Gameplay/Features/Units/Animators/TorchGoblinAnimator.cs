﻿using System;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Battle.Animators;
using Code.Gameplay.Features.Battle.Services;
using Code.Gameplay.Features.Move.Animators;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Units.Animators
{
  public class TorchGoblinAnimator : MonoBehaviour, IMoveAnimator, IAttackAnimator, IAnimationSpeedChanger
  {
    private const float FlipXMinValue = 0.1f;
    private static readonly int AnimationsSpeed = Animator.StringToHash("AnimationsSpeed");

    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public Vector2 LookDirection { get; private set; }

    private IAttackAnimationService _attackAnimationService;
    private int _unitId;
    private float _animationsSpeed = 1;

    [Inject]
    private void Construct(IAttackAnimationService attackService) =>
      _attackAnimationService = attackService;

    public void InitializeAttackAnimator(int unitId) =>
      _unitId = unitId;

    public void AnimateIdle() =>
      Animator.Play(AnimationConstants.Idle);

    public void AnimateWalk(Vector2 dir)
    {
      TurnToDirX(dir);
      Animator.Play(AnimationConstants.Walk);
      LookDirection = dir.normalized;
    }

    public void AnimateAttack(Vector2 dir)
    {
      SpriteRenderer.flipX = false;

      if (TargetOnY(dir))
        AnimateAttackByY(dir);
      else
        AnimateAttackByX(dir);

      LookDirection = dir.normalized;
    }

    public void UnitMakeHit() =>
      _attackAnimationService.UnitMakeHit(_unitId);

    public void UnitFinishedAttack() =>
      _attackAnimationService.UnitFinishedAttack(_unitId);

    private void AnimateAttackByX(Vector2 dir)
    {
      TurnToDirX(dir);
      Animator.Play(AnimationConstants.AttackRight);
    }

    private static bool TargetOnY(Vector2 dir) =>
      Mathf.Abs(dir.y) > Mathf.Abs(dir.x);

    private void AnimateAttackByY(Vector2 dir) =>
      Animator.Play(dir.y > 0 ? AnimationConstants.AttackTop : AnimationConstants.AttackBottom);

    private void TurnToDirX(Vector2 dir)
    {
      if (Mathf.Abs(dir.x) > FlipXMinValue)
        SpriteRenderer.flipX = dir.x < 0;
    }

    public void ChangeSpeed(float speed)
    {
      if (Math.Abs(_animationsSpeed - speed) <= float.Epsilon)
        return;

      _animationsSpeed = speed;
      Animator.SetFloat(AnimationsSpeed, speed);
    }
  }
}
