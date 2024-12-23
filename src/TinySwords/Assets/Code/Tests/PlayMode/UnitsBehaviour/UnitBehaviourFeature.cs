using Code.Gameplay.Features.AI;
using Code.Gameplay.Features.Battle;
using Code.Gameplay.Features.Command;
using Code.Gameplay.Features.Command.Systems;
using Code.Gameplay.Features.Death;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.HpBars;
using Code.Gameplay.Features.Indicators;
using Code.Gameplay.Features.Move;
using Code.Gameplay.Features.NavMesh;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Views;

namespace Code.Tests.PlayMode.UnitsBehaviour
{
  public sealed class UnitBehaviourFeature : Feature
  {
    public UnitBehaviourFeature(ISystemFactory systems)
    {
      Add(systems.Create<BindViewFeature>());
      
      Add(systems.Create<RemoveCompletedCommandFromSelectableSystem>());
      
      Add(systems.Create<AIFeature>());

      Add(systems.Create<NavMeshFeature>());
      Add(systems.Create<MoveFeature>());
      Add(systems.Create<IndicatorFeature>());
      
      Add(systems.Create<BattleFeature>());
      Add(systems.Create<EffectFeature>());
      Add(systems.Create<HpBarFeature>());
      
      Add(systems.Create<DeathFeature>());
      
      Add(systems.Create<ProcessDestructedFeature>());
    }
  }
}
