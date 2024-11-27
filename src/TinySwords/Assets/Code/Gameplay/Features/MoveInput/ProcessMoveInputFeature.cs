using Code.Gameplay.Features.MoveInput.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.MoveInput
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
