using Code.Gameplay.Features.Destruct.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Destruct
{
  public sealed class DestructFeature : Feature
  {
    public DestructFeature(ISystemFactory systems)
    {
      Add(systems.Create<DestructGameEntityViewSystem>());
      Add(systems.Create<DestructGameEntitySystem>());
    }
  }
}
