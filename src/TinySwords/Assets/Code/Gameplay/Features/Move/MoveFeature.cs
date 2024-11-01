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
      
      Add(systems.Create<SetMovePositionByClickSystem>());

      Add(systems.Create<UpdateMovementStateSystem>());
      Add(systems.Create<UpdateMoveDirectionSystem>());
      
      Add(systems.Create<UpdateWorldPositionSystem>());
      
      Add(systems.Create<AnimateIdleSystem>());
      Add(systems.Create<AnimateWalkSystem>());
      
      
      Add(systems.Create<CleanupPositionUpdatedComponentSystem>());
    }
  }
}
