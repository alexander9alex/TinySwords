using Code.Gameplay.Features.NearestEntitiesCollection.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.NearestEntitiesCollection
{
  public sealed class CollectNearestEntitiesFeature : Feature
  {
    public CollectNearestEntitiesFeature(ISystemFactory systems)
    {
      Add(systems.Create<TickToCollectTargetTimerSystem>());

      Add(systems.Create<CollectTargetsSystem>());
      Add(systems.Create<CollectReachedTargetsSystem>());
    }
  }
}
