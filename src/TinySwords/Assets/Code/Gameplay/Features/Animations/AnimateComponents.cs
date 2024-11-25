using Code.Gameplay.Features.Units.Animations.Animators;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Animations
{
  [Game] public class AnimateAttack : IComponent { }
  [Game] public class AttackAnimator : IComponent { public IAttackAnimator Value; }
  [Game] public class SelectingAnimator : IComponent { public ISelectingAnimator Value; }
  [Game] public class LookDirection : IComponent { public Vector2 Value; }
  [Game] public class MoveAnimator : IComponent { public IMoveAnimator Value; }
}
