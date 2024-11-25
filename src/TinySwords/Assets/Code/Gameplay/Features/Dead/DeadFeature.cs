using Code.Gameplay.Features.Dead.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Dead
{
  public sealed class DeadFeature : Feature
  {
    public DeadFeature(ISystemFactory systems)
    {
      Add(systems.Create<MarkDeadFeature>());
    }
  }
}
