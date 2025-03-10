﻿using Code.Gameplay.Features.Command.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Command
{
  public sealed class CommandFeature : Feature
  {
    public CommandFeature(ISystemFactory systems)
    {
      Add(systems.Create<RemoveCompletedCommandFromSelectableSystem>());
      
      Add(systems.Create<SelectCommandSystem>());
      Add(systems.Create<ApplyCommandSystem>());
      Add(systems.Create<CancelCommandSystem>());
      
      Add(systems.Create<CleanupProcessCommandRequestSystem>());
      Add(systems.Create<CleanupProcessIncorrectCommandSystem>());
    }
  }
}
