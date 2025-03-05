using System;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Battle.Animators;
using Code.Gameplay.Features.Battle.Services;
using Code.Gameplay.Features.Interactions.Select.Animators;
using Code.Gameplay.Features.Move.Animators;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Units.Animators
{
  public class KnightAnimator : MonoBehaviour, ISelectingAnimator, IMoveAnimator, IAttackAnimator, IAnimationSpeedChanger
  {
    private const float FlipXMinValue = 0.1f;
    private static readonly int AnimationsSpeed = Animator.StringToHash("AnimationsSpeed");

    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public GameObject SelectingCircle;
    public Vector2 LookDirection { get; private set; }

    private int _unitId;
    private IAttackAnimationService _attackAnimationService;
    private float _animationSpeed = 1;

    [Inject]
    private void Construct(IAttackAnimationService attackAnimationService) =>
      _attackAnimationService = attackAnimationService;

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

    public void AnimateSelecting() =>
      SelectingCircle.SetActive(true);

    public void AnimateUnselecting() =>
      SelectingCircle.SetActive(false);

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
      if (Math.Abs(_animationSpeed - speed) <= float.Epsilon)
        return;

      _animationSpeed = speed;
      Animator.SetFloat(AnimationsSpeed, speed);
    }
  }
}
