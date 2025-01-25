using Code.Gameplay.Features.ProcessCommand.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.ProcessCommand
{
  public sealed class ProcessCommandFeature : Feature
  {
    public ProcessCommandFeature(ISystemFactory systems)
    {
      Add(systems.Create<ProcessMoveCommandSystem>());
      Add(systems.Create<ProcessMoveWithAttackCommandSystem>());
      Add(systems.Create<ProcessAimedAttackCommandSystem>());
      Add(systems.Create<ProcessIncorrectCommandSystem>());

      Add(systems.Create<OffsetPositionByLegsSystem>());
    }
  }
}
