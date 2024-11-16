using Code.Gameplay.Features.ControlAction.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.ControlAction
{
  public sealed class ControlActionFeature : Feature
  {
    public ControlActionFeature(ISystemFactory systems)
    {
      Add(systems.Create<SelectControlActionSystem>());
      
      Add(systems.Create<ApplyMoveControlActionSystem>());
      Add(systems.Create<ApplyMoveWithAttackControlActionSystem>());
      
      Add(systems.Create<CancelControlActionSystem>());

      Add(systems.Create<CleanupApplyControlActionRequestSystem>());
    }
  }
}
