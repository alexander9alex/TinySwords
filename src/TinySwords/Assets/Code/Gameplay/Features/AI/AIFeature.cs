using Code.Gameplay.Features.AI.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.AI
{
  public sealed class AIFeature : Feature
  {
    public AIFeature(ISystemFactory systems)
    {
      Add(systems.Create<TickToUpdateFieldOfVisionTimerSystem>());
      Add(systems.Create<UpdateFieldOfVisionNowSystem>());
      Add(systems.Create<UpdateFieldOfVisionSystem>());
      
      Add(systems.Create<CollectTargetsSystem>());
      Add(systems.Create<CollectReachedTargetsSystem>());
      Add(systems.Create<CollectAlliesSystem>());

      Add(systems.Create<UnitMakeDecisionSystem>());
      Add(systems.Create<RemoveAllUnitDecisionsSystem>());
      Add(systems.Create<ProcessUnitDecisionSystem>());

      Add(systems.Create<NotifyAlliesAboutTargetSystem>());
    }
  }
}
