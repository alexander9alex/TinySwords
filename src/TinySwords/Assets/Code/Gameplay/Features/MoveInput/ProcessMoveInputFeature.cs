using Code.Gameplay.Features.MoveInput.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.MoveInput
{
  public sealed class ProcessMoveInputFeature : Feature
  {
    public ProcessMoveInputFeature(ISystemFactory systems)
    {
      Add(systems.Create<ConvertPositionOnEndDestinationRequestSystem>());
      Add(systems.Create<ConvertEndDestinationSystem>());
      
      Add(systems.Create<ChangeEndDestinationAtAllSelectedSystem>());
      Add(systems.Create<ChangeEndDestinationByIdSystem>());
      Add(systems.Create<ChangeEndDestinationWhenHasAimedTargetSystem>());
      
      Add(systems.Create<CleanupEndDestinationSystem>());
      Add(systems.Create<CleanupEndDestinationRequestSystem>());
    }
  }
}
