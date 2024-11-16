using Code.Gameplay.Features.FastInteract.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.FastInteract
{
  public sealed class FastInteractFeature : Feature
  {
    public FastInteractFeature(ISystemFactory systems)
    {
      // Add(systems.Crate<PickEntitiesForFastInteractionSystem>());

      // Add(systems.Create<CreateFastInteractWithUnitRequestSystem>());
      // Add(systems.Create<CreateMoveRequestSystem>());
    }
  }
}
