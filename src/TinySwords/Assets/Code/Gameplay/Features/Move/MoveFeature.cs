using Code.Gameplay.Features.Move.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Move
{
  public sealed class MoveFeature : Feature
  {
    public MoveFeature(ISystemFactory systems)
    {
      Add(systems.Create<UpdateTransformSystem>());
    }
  }
}
