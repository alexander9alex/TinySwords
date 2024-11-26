using Code.Gameplay.Features.Input.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Input
{
  public sealed class InputFeature : Feature
  {
    public InputFeature(ISystemFactory systems)
    {
      Add(systems.Create<SetActionEndedProcessingSystem>());
     
      Add(systems.Create<CreateHighlightRequestSystem>());
      Add(systems.Create<UpdateHighlightLocationRequestSystem>());
      
      Add(systems.Create<CreateSingleSelectionRequestSystem>());
      Add(systems.Create<CreateMultipleSelectionRequestSystem>());

      Add(systems.Create<CreateFocusRequestSystem>());
      
      Add(systems.Create<CleanupMousePositionSystem>());
      
      Add(systems.Create<CleanupActionInputSystem>());
    }
  }
}
