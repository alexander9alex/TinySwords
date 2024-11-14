using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Gameplay.Common.Services;
using Code.UI.Buttons.Configs;
using Code.UI.Buttons.Data;

namespace Code.UI.Hud.Service
{
  class HudService : IHudService
  {
    public event Action UpdateHud;

    private readonly IStaticDataService _staticData;
    private List<ControlButtonConfig> _availableButtonConfigs = new();

    public HudService(IStaticDataService staticData) =>
      _staticData = staticData;

    public void UpdateAvailableActions(List<ActionTypeId> availableActions)
    {
      if (Equals(_availableButtonConfigs.Select(x => x.ActionTypeId), availableActions))
        return;
      
      _availableButtonConfigs = _staticData.GetControlButtonConfigs(availableActions);
      UpdateHud?.Invoke();
    }

    public List<ControlButtonConfig> GetAvailableButtonConfigs() =>
      _availableButtonConfigs;

    public void ClickedToButton(ActionTypeId actionTypeId) =>
      CreateEntity.Empty()
        .AddActionTypeId(actionTypeId);
  }
}
