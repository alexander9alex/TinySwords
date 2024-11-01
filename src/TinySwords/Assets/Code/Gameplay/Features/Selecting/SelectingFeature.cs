using Code.Gameplay.Features.Selecting.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Selecting
{
  public sealed class SelectingFeature : Feature
  {
    public SelectingFeature(ISystemFactory systems)
    {
      Add(systems.Create<SelectByClickSystem>());
      
      Add(systems.Create<UnselectPreviouslySelectedSystem>());
      Add(systems.Create<DeselectIfClickNotProcessedSystem>());

      Add(systems.Create<AnimateSelectingSystem>());
      Add(systems.Create<AnimateUnselectingSystem>());
      
      Add(systems.Create<CleanupSelectedNowSystem>());
      Add(systems.Create<CleanupUnselectRequestSystem>());
    }
  }
}
