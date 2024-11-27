using Code.Gameplay.Features.Effects.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Effects
{
  public sealed class EffectFeature : Feature
  {
    public EffectFeature(ISystemFactory systems)
    {
      Add(systems.Create<ProcessDamageEffectSystem>());
      
      Add(systems.Create<AnimateTakingDamageSystem>());
    }
  }
}
