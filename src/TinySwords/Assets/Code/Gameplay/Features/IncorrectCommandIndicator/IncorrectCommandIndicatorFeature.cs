using Code.Gameplay.Features.IncorrectCommandIndicator.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.IncorrectCommandIndicator
{
  public sealed class IncorrectCommandIndicatorFeature : Feature
  {
    public IncorrectCommandIndicatorFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateIncorrectCommandIndicatorSystem>());
      Add(systems.Create<DestructOldIncorrectCommandIndicatorSystem>());
      
      Add(systems.Create<CleanupCreatedNowIncorrectCommandIndicatorSystem>());
    }
  }
}
