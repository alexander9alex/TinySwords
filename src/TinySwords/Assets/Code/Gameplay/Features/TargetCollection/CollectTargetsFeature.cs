using Code.Gameplay.Features.TargetCollection.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.TargetCollection
{
  public sealed class CollectTargetsFeature : Feature
  {
    public CollectTargetsFeature(ISystemFactory systems)
    {
      Add(systems.Create<TickToCollectTargetTimerSystem>());

      Add(systems.Create<CollectTargetsSystem>());
      Add(systems.Create<CollectReachedTargetsSystem>());
    }
  }
}
