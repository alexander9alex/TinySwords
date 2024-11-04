using Code.Gameplay.Features.Destruct.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Destruct
{
  public sealed class ProcessDestructedFeature : Feature
  {
    public ProcessDestructedFeature(ISystemFactory systems)
    {
      Add(systems.Create<DestructAfterTimeSystem>());
      
      Add(systems.Create<CleanupDestructedViewSystem>());
      Add(systems.Create<CleanupDestructedSystem>());
    }
  }
}
