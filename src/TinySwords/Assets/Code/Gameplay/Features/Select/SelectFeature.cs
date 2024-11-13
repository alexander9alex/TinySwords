using Code.Gameplay.Features.Select.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Select
{
  public sealed class SelectFeature : Feature
  {
    public SelectFeature(ISystemFactory systems)
    {
      Add(systems.Create<ProcessSingleSelectionSystem>());
      Add(systems.Create<SelectHighlightedSystem>());
      
      Add(systems.Create<UnselectPreviouslySelectedSystem>());
      // Add(systems.Create<UnselectIfSingleSelectionNotProcessedSystem>());

      Add(systems.Create<AnimateSelectingSystem>());
      Add(systems.Create<AnimateUnselectingSystem>());
      
      Add(systems.Create<CleanupSelectedNowSystem>());
      Add(systems.Create<CleanupUnselectRequestSystem>());
    }
  }
}
