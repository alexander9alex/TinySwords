using Code.Gameplay.Features.Units.Animations.Animators;
using Code.Gameplay.Features.Units.Data;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Units
{
  [Game] public class Unit : IComponent { }
  [Game] public class Speed : IComponent { public float Value; }
  [Game] public class Damage : IComponent { public float Value; }
  [Game] public class CurrentHp : IComponent { public float Value; }
  [Game] public class MaxHp : IComponent { public float Value; }
  [Game] public class AttackReach : IComponent { public float Value; }
  [Game] public class TeamColorComponent : IComponent { public TeamColor Value; }
  
  [Game] public class LookDirection : IComponent { public Vector2 Value; }
  [Game] public class MoveAnimator : IComponent { public IMoveAnimator Value; }
}
