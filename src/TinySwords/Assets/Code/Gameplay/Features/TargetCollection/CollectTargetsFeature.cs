using Code.Gameplay.Features.TargetCollection.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.TargetCollection
{
  public sealed class CollectTargetsFeature : Feature
  {
    public CollectTargetsFeature(ISystemFactory systems)
    {
      Add(systems.Create<TickToCollectTargetsTimerSystem>());
      
      Add(systems.Create<CollectTargetsSystem>());
    }
  }
}
