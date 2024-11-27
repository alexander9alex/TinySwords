using Code.Gameplay.Features.Command.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Command
{
  public sealed class CommandFeature : Feature
  {
    public CommandFeature(ISystemFactory systems)
    {
      Add(systems.Create<SelectCommandSystem>());
      Add(systems.Create<ApplyCommandSystem>());
      Add(systems.Create<CancelCommandSystem>());

      Add(systems.Create<ProcessCommandSystem>());
      
      Add(systems.Create<CleanupApplyCommandRequestSystem>());
    }
  }
}
