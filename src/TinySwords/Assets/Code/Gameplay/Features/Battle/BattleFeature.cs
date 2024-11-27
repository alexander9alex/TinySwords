using Code.Gameplay.Features.Battle.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Battle
{
  public sealed class BattleFeature : Feature
  {
    public BattleFeature(ISystemFactory systems)
    {
      Add(systems.Create<InitializeAttackAnimatorSystem>());

      Add(systems.Create<FinishAttackSystem>());
      Add(systems.Create<TickToAttackCooldownSystem>());
      
      Add(systems.Create<ProcessUnitAttackRequestSystem>());
      Add(systems.Create<AnimateAttackSystem>());

      Add(systems.Create<MakeHitSystem>());
    }
  }
}
