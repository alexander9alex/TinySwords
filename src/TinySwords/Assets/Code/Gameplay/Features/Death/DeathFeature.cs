using Code.Gameplay.Features.Death.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Death
{
  public sealed class DeathFeature : Feature
  {
    public DeathFeature(ISystemFactory systems)
    {
      Add(systems.Create<MarkDeadFeature>());
      Add(systems.Create<CreateDeathAnimationSystem>());

      Add(systems.Create<AnimateUnitDeathSystem>());
      
      Add(systems.Create<CleanupDeadEntitiesSystem>());
    }
  }
}
