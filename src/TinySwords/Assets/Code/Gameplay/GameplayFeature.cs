using Code.Gameplay.Features.Build;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Highlight;
using Code.Gameplay.Features.Input;
using Code.Gameplay.Features.Interact;
using Code.Gameplay.Features.Move;
using Code.Gameplay.Features.Select;
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
      Add(systems.Create<InteractFeature>());
      
      Add(systems.Create<HighlightFeature>());
      Add(systems.Create<SelectFeature>());
      
      Add(systems.Create<BuildFeature>());

      Add(systems.Create<MoveFeature>());
      
      Add(systems.Create<ProcessDestructedFeature>());
    }
  }
}
