using Code.Gameplay.Features.MoveIndicator.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.MoveIndicator
{
  public sealed class MoveIndicatorFeature : Feature
  {
    public MoveIndicatorFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateMoveClickIndicatorSystem>());
      Add(systems.Create<DestructOldClickIndicatorSystem>());
      
      Add(systems.Create<CleanupCreatedNowSystem>());
    }
  }
}
