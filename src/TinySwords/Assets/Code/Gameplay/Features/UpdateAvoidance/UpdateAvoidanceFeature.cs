using Code.Gameplay.Features.UpdateAvoidance.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.UpdateAvoidance
{
  public sealed class UpdateAvoidanceFeature : Feature
  {
    public UpdateAvoidanceFeature(ISystemFactory systems)
    {
      Add(systems.Create<UpdateMoveAvoidanceSystem>());
      Add(systems.Create<UpdateIdleAvoidanceSystem>());
      Add(systems.Create<SetAvoidanceSystem>());
    }
  }
}
