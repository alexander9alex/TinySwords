using Code.Gameplay.Features.AI;
using Code.Gameplay.Features.Battle;
using Code.Gameplay.Features.Cameras;
using Code.Gameplay.Features.CollectEntities;
using Code.Gameplay.Features.Command;
using Code.Gameplay.Features.Death;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.FastInteractions;
using Code.Gameplay.Features.Focus;
using Code.Gameplay.Features.FogOfWar;
using Code.Gameplay.Features.HpBars;
using Code.Gameplay.Features.Indicators;
using Code.Gameplay.Features.Input;
using Code.Gameplay.Features.Interactions;
using Code.Gameplay.Features.Lose;
using Code.Gameplay.Features.Move;
using Code.Gameplay.Features.NavMesh;
using Code.Gameplay.Features.ProcessCommand;
using Code.Gameplay.Features.Sounds;
using Code.Gameplay.Features.Units;
using Code.Gameplay.Features.Win;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Views;

namespace Code.Gameplay
{
  public sealed class GameplayFeature : Feature
  {
    public GameplayFeature(ISystemFactory systems)
    {
      Add(systems.Create<BindViewFeature>());
      Add(systems.Create<InputFeature>());

      Add(systems.Create<InteractionFeature>());
      Add(systems.Create<FastInteractionFeature>());
      
      Add(systems.Create<CommandFeature>());
      Add(systems.Create<ProcessCommandFeature>());
      
      Add(systems.Create<FocusFeature>());
      
      Add(systems.Create<CollectEntitiesFeature>());
      Add(systems.Create<AIFeature>());
      Add(systems.Create<UnitsFeature>());

      Add(systems.Create<NavMeshFeature>());
      Add(systems.Create<MoveFeature>());
      Add(systems.Create<IndicatorFeature>());
      
      Add(systems.Create<BattleFeature>());
      Add(systems.Create<EffectFeature>());
      Add(systems.Create<HpBarFeature>());
      
      Add(systems.Create<WinFeature>());
      Add(systems.Create<LoseFeature>());
      
      Add(systems.Create<FogOfWarFeature>());

      Add(systems.Create<CameraFeature>());
      
      Add(systems.Create<DeathFeature>());
      
      Add(systems.Create<SoundFeature>());
      
      Add(systems.Create<ProcessDestructedFeature>());
    }
  }
}
