using Code.Gameplay.Features.Move.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Move
{
  public sealed class MoveFeature : Feature
  {
    public MoveFeature(ISystemFactory systems)
    {
      Add(systems.Create<UpdateTransformAfterSpawningSystem>());
      Add(systems.Create<TurnOnNavMeshAgentAfterTransformUpdatingSystem>());

      Add(systems.Create<ProcessMoveRequestSystem>());
      Add(systems.Create<MoveToDestinationSystem>());

      Add(systems.Create<CreateMoveClickIndicatorSystem>());
      Add(systems.Create<DestructOldClickIndicatorSystem>());
      
      Add(systems.Create<UpdateMoveDirectionSystem>());
      Add(systems.Create<UpdateMovementStateSystem>());

      Add(systems.Create<UpdateMoveAvoidanceSystem>());
      Add(systems.Create<UpdateIdleAvoidanceSystem>());
      Add(systems.Create<SetAvoidanceSystem>());

      Add(systems.Create<UpdateWorldPositionSystem>());

      Add(systems.Create<AnimateIdleSystem>());
      Add(systems.Create<AnimateMoveSystem>());

      Add(systems.Create<CleanupDestinationSystem>());
      Add(systems.Create<CleanupMoveRequestSystem>());
      Add(systems.Create<CleanupCreatedNowSystem>());
      Add(systems.Create<CleanupPositionUpdatedSystem>());
      Add(systems.Create<CleanupDestructOldClickIndicatorRequestSystem>());
    }
  }
}
