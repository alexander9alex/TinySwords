using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Command.Configs;
using Code.Gameplay.Features.Command.Data;
using UnityEngine;

namespace Code.UI.Hud.Service
{
  class HudService : IHudService
  {
    public event Action UpdateCommandButtons;
    public event Action UpdateCommandDescription;
    public List<CommandUIConfig> AvailableCommandUIConfigs { get; private set; } = new();
    public GameObject CommandDescriptionPrefab { get; private set; }

    private readonly IStaticDataService _staticData;

    public HudService(IStaticDataService staticData) =>
      _staticData = staticData;

    public void UpdateAvailableCommands(List<CommandTypeId> commands)
    {
      if (Equals(AvailableCommandUIConfigs.Select(x => x.CommandTypeId), commands))
        return;

      AvailableCommandUIConfigs = _staticData.GetUnitCommandUIConfigs(commands);

      if (CommandDescriptionPrefab == null)
        UpdateCommandButtons?.Invoke();
    }

    public void SelectCommand(CommandTypeId commandTypeId)
    {
      AvailableCommandUIConfigs = new();
      UpdateCommandButtons?.Invoke();

      CommandDescriptionPrefab = _staticData.GetCommandUIConfig(commandTypeId).CommandDescriptionPrefab;
      UpdateCommandDescription?.Invoke();
    }

    public void ClickByCommand(CommandTypeId commandTypeId)
    {
      CreateEntity.Empty()
        .AddCommandTypeId(commandTypeId)
        .With(x => x.isCommand = true);
    }

    public void CancelCommand()
    {
      CommandDescriptionPrefab = null;
      UpdateCommandDescription?.Invoke();
    }
  }
}
