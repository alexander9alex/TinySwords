using Code.Infrastructure.Factory;
using Code.Infrastructure.Views.Systems;

namespace Code.Infrastructure.Views
{
  public sealed class BindViewFeature : Feature
  {
    public BindViewFeature(ISystemFactory systems)
    {
      Add(systems.Create<BindEntityViewFromPrefabSystem>());
      Add(systems.Create<InitViewPositionSystem>());
      Add(systems.Create<InitSpeedSystem>());
      
      Add(systems.Create<CleanupInitializeMarkerSystem>());
    }
  }
}
