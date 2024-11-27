using Code.Gameplay.Features.Effects.Animators;
using Code.Gameplay.Features.Effects.Data;
using Entitas;

namespace Code.Gameplay.Features.Effects
{
  [Game] public class DamageEffect : IComponent { }
  [Game] public class EffectTypeIdComponent : IComponent { public EffectTypeId Value; }
  [Game] public class EffectValue : IComponent { public float Value; }
  [Game] public class TargetId : IComponent { public int Value; }
  [Game] public class CasterId : IComponent { public int Value; }
  [Game] public class AnimateTakenDamage : IComponent { }
  [Game] public class DamageTakenAnimator : IComponent { public IDamageTakenAnimator Value; }
}
