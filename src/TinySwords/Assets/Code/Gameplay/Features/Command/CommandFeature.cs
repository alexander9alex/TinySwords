using Code.Gameplay.Features.Command.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Command
{
  public sealed class CommandFeature : Feature
  {
    public CommandFeature(ISystemFactory systems)
    {
      Add(systems.Create<RemoveCompletedCommandFromSelectableSystem>()); // todo: move?
      
      Add(systems.Create<SelectCommandSystem>());
      Add(systems.Create<ApplyCommandSystem>());
      Add(systems.Create<CancelCommandSystem>());

      Add(systems.Create<ProcessMoveCommandSystem>()); // todo: refactor
      Add(systems.Create<ProcessMoveWithAttackCommandSystem>()); // todo: refactor
      Add(systems.Create<ProcessAimedAttackCommandSystem>()); // todo: refactor

      Add(systems.Create<ProcessIncorrectAimedAttackCommandSystem>());
      
      Add(systems.Create<RemovePreviousCommandFromSelectableSystem>()); // todo: move?

      Add(systems.Create<CleanupApplyCommandRequestSystem>());
      Add(systems.Create<CleanupProcessCommandRequestSystem>());
      Add(systems.Create<CleanupProcessIncorrectCommandSystem>());
    }
  }
}
