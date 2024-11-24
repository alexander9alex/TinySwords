using Code.Gameplay.Features.Select.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Select
{
  public sealed class SelectFeature : Feature
  {
    public SelectFeature(ISystemFactory systems)
    {
      Add(systems.Create<ProcessSingleSelectionSystem>());
      Add(systems.Create<SelectHighlightedUnitSystem>());
      Add(systems.Create<SelectHighlightedCastleSystem>());
      
      Add(systems.Create<UnselectPreviouslySelectedSystem>());
      Add(systems.Create<ProcessSelectedChangingSystem>());
      Add(systems.Create<UpdateHudControlButtonSystem>());

      Add(systems.Create<AnimateSelectingSystem>());
      Add(systems.Create<AnimateUnselectingSystem>());
      
      Add(systems.Create<CleanupSelectedNowSystem>());
      Add(systems.Create<CleanupUnselectRequestSystem>());
      
      Add(systems.Create<CleanupSingleSelectionRequestSystem>());
      Add(systems.Create<CleanupMultipleSelectionRequestSystem>());
      Add(systems.Create<CleanupSelectedChangedRequestSystem>());
    }
  }
}
