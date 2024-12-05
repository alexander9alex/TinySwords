using System;
using System.Collections.Generic;
using Code.Gameplay.Features.Command.Configs;
using Code.Gameplay.Features.Command.Data;
using UnityEngine;

namespace Code.UI.Hud.Service
{
  public interface IHudService
  {
    event Action UpdateCommandButtons;
    event Action UpdateCommandDescription;
    List<CommandUIConfig> AvailableCommandUIConfigs { get; }
    GameObject CommandDescriptionPrefab { get; }
    void UpdateAvailableCommands(List<CommandTypeId> commands);
    void SelectCommand(CommandTypeId commandTypeId);
    void ClickByCommand(CommandTypeId commandTypeId);
    void CancelCommand();
  }
}
