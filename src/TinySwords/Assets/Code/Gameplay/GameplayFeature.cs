using Code.Gameplay.Features.AI;
using Code.Gameplay.Features.Animations;
using Code.Gameplay.Features.Battle;
using Code.Gameplay.Features.Build;
using Code.Gameplay.Features.ControlAction;
using Code.Gameplay.Features.Dead;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.FastInteraction;
using Code.Gameplay.Features.Highlight;
using Code.Gameplay.Features.Input;
using Code.Gameplay.Features.Move;
using Code.Gameplay.Features.MoveIndicator;
using Code.Gameplay.Features.NavMesh;
using Code.Gameplay.Features.Select;
using Code.Gameplay.Features.TargetCollection;
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
      Add(systems.Create<ControlActionFeature>());
      Add(systems.Create<FastInteractionFeature>());

      Add(systems.Create<HighlightFeature>());
      Add(systems.Create<SelectFeature>());
      
      Add(systems.Create<BuildFeature>());
      
      Add(systems.Create<NavMeshFeature>());
      Add(systems.Create<MoveFeature>());
      Add(systems.Create<MoveIndicatorFeature>());
      
      Add(systems.Create<CollectTargetsFeature>());
      Add(systems.Create<BattleFeature>());
      Add(systems.Create<EffectFeature>());
      
      Add(systems.Create<AnimateFeature>());
      
      Add(systems.Create<DeadFeature>());

      Add(systems.Create<AIFeature>());
      
      Add(systems.Create<ProcessDestructedFeature>());
    }
  }
}
