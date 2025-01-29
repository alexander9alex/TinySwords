using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Sounds;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Views;

namespace Code.Gameplay
{
  public class CutSceneFeature : Feature
  {
    public CutSceneFeature(ISystemFactory systems)
    {
      Add(systems.Create<BindViewFeature>());
      
      Add(systems.Create<SoundFeature>());
      
      Add(systems.Create<ProcessDestructedFeature>());
    }
  }
}