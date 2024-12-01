using Code.Gameplay.Features.AttackIndicator.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.AttackIndicator
{
  public sealed class AttackIndicatorFeature : Feature
  {
    public AttackIndicatorFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateAttackIndicatorSystem>());
      Add(systems.Create<DestructOldAttackIndicatorSystem>());
      
      Add(systems.Create<CleanupCreatedNowAttackIndicatorSystem>());
    }
  }
}
