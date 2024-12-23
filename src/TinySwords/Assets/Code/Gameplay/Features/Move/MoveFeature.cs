using Code.Gameplay.Features.Move.Systems;
using Code.Gameplay.Features.Units;
using Code.Gameplay.Features.UpdateAvoidance;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Move
{
  public sealed class MoveFeature : Feature
  {
    public MoveFeature(ISystemFactory systems)
    {
      Add(systems.Create<FollowToTargetSystem>());
      Add(systems.Create<TeleportToTargetSystem>());
      Add(systems.Create<MoveToDestinationSystem>());
      Add(systems.Create<CalculateSpeedSystem>());

      Add(systems.Create<UpdateWorldPositionSystem>());

      Add(systems.Create<UpdateMoveDirectionSystem>());
      Add(systems.Create<UpdateMovementStateSystem>());

      Add(systems.Create<AnimateIdleSystem>());
      Add(systems.Create<AnimateMoveSystem>());
      Add(systems.Create<UpdateLookDirectionSystem>());

      Add(systems.Create<UpdateAvoidanceFeature>());

      Add(systems.Create<MoveCameraSystem>());

      Add(systems.Create<CleanupDestinationSystem>());
    }
  }
}
