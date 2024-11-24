using Code.Gameplay.Features.Effects.Data;
using Entitas;

namespace Code.Gameplay.Features.Effects
{
  [Game] public class EffectTypeIdComponent : IComponent { public EffectTypeId Value; }
  [Game] public class EffectValue : IComponent { public float Value; }
  [Game] public class TargetId : IComponent { public int Value; }
}
