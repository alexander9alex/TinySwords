using Code.Gameplay.Features.Selecting.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Selecting
{
  public sealed class SelectingFeature : Feature
  {
    public SelectingFeature(ISystemFactory systems)
    {
      Add(systems.Create<SelectByClickSystem>());
    }
  }
}
