using Code.Gameplay.Features.Interactions.Select.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Interactions.Select
{
  public sealed class SelectFeature : Feature
  {
    public SelectFeature(ISystemFactory systems)
    {
      Add(systems.Create<ProcessSingleSelectionSystem>());
      Add(systems.Create<SelectHighlightedUnitsSystem>());
      
      Add(systems.Create<UnselectPreviouslySelectedSystem>());
      Add(systems.Create<UpdateHudControlButtonSystem>());
      
      Add(systems.Create<AnimateSelectingSystem>());
      Add(systems.Create<AnimateUnselectingSystem>());
      
      Add(systems.Create<CleanupSelectedNowSystem>());
    }
  }
}
