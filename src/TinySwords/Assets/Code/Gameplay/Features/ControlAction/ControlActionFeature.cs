using Code.Gameplay.Features.ControlAction.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.ControlAction
{
  public sealed class ControlActionFeature : Feature
  {
    public ControlActionFeature(ISystemFactory systems)
    {
      Add(systems.Create<SelectControlActionSystem>());
      Add(systems.Create<CancelControlActionSystem>());
    }
  }
}
