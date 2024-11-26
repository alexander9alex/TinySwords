using Code.Gameplay.Features.HpBars.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.HpBars
{
  public sealed class HpBarFeature : Feature
  {
    public HpBarFeature(ISystemFactory systems)
    {
      Add(systems.Create<HideHpBarSystem>());
      Add(systems.Create<UpdateHpBarSystem>());
      Add(systems.Create<ShowHpBarSystem>());
    }
  }
}
