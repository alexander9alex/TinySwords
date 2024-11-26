using Code.Gameplay.Features.Battle.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Battle
{
  public sealed class BattleFeature : Feature
  {
    public BattleFeature(ISystemFactory systems)
    {
      Add(systems.Create<FinishAttackSystem>());
      
      Add(systems.Create<TickToAttackCooldownSystem>());
      Add(systems.Create<ProcessUnitAttackRequestSystem>());
      
      Add(systems.Create<MakeHitSystem>());
      
      Add(systems.Create<UpdateUnitAttackStateSystem>());
    }
  }
}
