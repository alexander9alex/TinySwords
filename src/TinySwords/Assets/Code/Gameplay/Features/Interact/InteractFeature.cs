using Code.Gameplay.Features.Interact.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Interact
{
  public sealed class InteractFeature : Feature
  {
    public InteractFeature(ISystemFactory systems)
    {
      Add(systems.Create<PickEntitiesForInteractionSystem>());

      Add(systems.Create<CreateInteractWithBuildRequestSystem>());
      Add(systems.Create<CreateInteractWithUnitRequestSystem>());
      Add(systems.Create<CreateMoveRequestSystem>());
      
      Add(systems.Create<CleanupInteractRequestSystem>());
    }
  }
}
