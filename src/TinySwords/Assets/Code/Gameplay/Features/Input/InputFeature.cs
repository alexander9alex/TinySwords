using Code.Gameplay.Features.Input.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Input
{
  public sealed class InputFeature : Feature
  {
    public InputFeature(ISystemFactory systems)
    {
      Add(systems.Create<InitializeInputSystem>());

      Add(systems.Create<CleanupInteractionStartInputSystem>());
      Add(systems.Create<CleanupInteractionEndInputSystem>());
      
      Add(systems.Create<CleanupApplyCommandInputSystem>());
      Add(systems.Create<CleanupFastInteractionInputSystem>());
    }
  }
}
