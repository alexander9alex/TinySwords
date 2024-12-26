using Code.Gameplay.Features.CollectEntities.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.CollectEntities
{
  public sealed class CollectEntitiesFeature : Feature
  {
    public CollectEntitiesFeature(ISystemFactory systems)
    {
      Add(systems.Create<TickToUpdateFieldOfVisionTimerSystem>());
      Add(systems.Create<UpdateFieldOfVisionNowSystem>());
      Add(systems.Create<UpdateFieldOfVisionSystem>());
      
      Add(systems.Create<CollectTargetsSystem>());
      Add(systems.Create<CollectReachedTargetsSystem>());
      Add(systems.Create<CollectAlliesSystem>());
    }
  }
}
