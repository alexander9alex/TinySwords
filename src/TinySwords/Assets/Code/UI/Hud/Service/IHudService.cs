using System;
using System.Collections.Generic;
using Code.UI.Buttons.Configs;
using Code.UI.Buttons.Data;
using UnityEngine;

namespace Code.UI.Hud.Service
{
  public interface IHudService
  {
    event Action UpdateHud;
    event Action UpdateActionDescription;
    GameObject GetActionDescription();
    List<ControlButtonConfig> GetAvailableButtonConfigs();
    void UpdateAvailableActions(List<ControlActionTypeId> availableActions);
    void ClickedToButton(ControlActionTypeId controlActionTypeId);
    void SetAction(ControlActionTypeId controlActionTypeId);
    void CancelAction();
  }
}
