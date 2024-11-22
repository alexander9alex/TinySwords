using Code.Gameplay.Features.AI.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.AI
{
  public sealed class AIFeature : Feature
  {
    public AIFeature(ISystemFactory systems)
    {
      Add(systems.Create<TickToMakeDecisionTimerSystem>());
      
      Add(systems.Create<UnitMakeDecisionSystem>());
    }
  }
}
