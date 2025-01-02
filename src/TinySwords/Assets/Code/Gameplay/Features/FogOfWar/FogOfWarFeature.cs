using Code.Gameplay.Features.FogOfWar.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.FogOfWar
{
  public sealed class FogOfWarFeature : Feature
  {
    public FogOfWarFeature(ISystemFactory systems)
    {
      Add(systems.Create<ClearGlowingObjectsSystem>());
      Add(systems.Create<SetGlowingObjectsSystem>());
      Add(systems.Create<UpdateFogOfWarSystem>());
    }
  }
}
