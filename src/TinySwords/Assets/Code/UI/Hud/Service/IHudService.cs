using System;
using System.Collections.Generic;
using Code.UI.Buttons.Configs;
using Code.UI.Buttons.Data;

namespace Code.UI.Hud.Service
{
  public interface IHudService
  {
    event Action UpdateHud;
    void UpdateAvailableActions(List<ActionTypeId> availableActions);
    List<ControlButtonConfig> GetAvailableButtonConfigs();
    void ClickedToButton(ActionTypeId actionTypeId);
  }
}
