using Code.Gameplay.Features.ChangeEndDestination.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.ChangeEndDestination
{
  public sealed class ProcessMoveInputFeature : Feature
  {
    public ProcessMoveInputFeature(ISystemFactory systems)
    {
      Add(systems.Create<ChangeEndDestinationSystem>());
      Add(systems.Create<UpdateRunAwayStateSystem>());
    }
  }
}
