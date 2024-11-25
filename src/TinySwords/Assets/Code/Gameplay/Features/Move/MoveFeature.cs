using Code.Gameplay.Features.Move.Systems;
using Code.Gameplay.Features.UpdateAvoidance;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Move
{
  public sealed class MoveFeature : Feature
  {
    public MoveFeature(ISystemFactory systems)
    {
      Add(systems.Create<ChangeEndDestinationSystem>());
      Add(systems.Create<MoveToDestinationSystem>());

      Add(systems.Create<UpdateMoveDirectionSystem>());
      Add(systems.Create<UpdateMovementStateSystem>());
      Add(systems.Create<UpdateWorldPositionSystem>());

      Add(systems.Create<UpdateAvoidanceFeature>());
      
      Add(systems.Create<CleanupDestinationSystem>());
      Add(systems.Create<CleanupEndDestinationSystem>());
    }
  }
}
