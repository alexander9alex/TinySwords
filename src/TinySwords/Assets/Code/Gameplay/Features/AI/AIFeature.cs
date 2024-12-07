using Code.Gameplay.Features.AI.Systems;
using Code.Gameplay.Features.Battle.Systems;
using Code.Gameplay.Features.TargetCollection.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.AI
{
  public sealed class AIFeature : Feature
  {
    public AIFeature(ISystemFactory systems)
    {
      Add(systems.Create<TickToMakeDecisionTimerSystem>());
      
      Add(systems.Create<CollectAlliesSystem>());

      Add(systems.Create<UnitMakeDecisionSystem>());
      Add(systems.Create<ProcessUnitDecisionSystem>());
    }
  }
}
