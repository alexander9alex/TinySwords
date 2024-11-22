using System;
using System.Collections.Generic;
using Code.Gameplay.Features.ControlAction.Configs;
using Code.Gameplay.Features.ControlAction.Data;
using UnityEngine;

namespace Code.UI.Hud.Service
{
  public interface IHudService
  {
    event Action UpdateHud;
    event Action UpdateActionDescription;
    GameObject GetActionDescription();
    List<UnitActionUIConfig> GetAvailableUnitActionUIConfigs();
    void UpdateAvailableActions(List<UnitCommandTypeId> availableCommands);
    void ClickedToButton(UnitCommandTypeId unitCommandTypeId);
    void SetAction(UnitCommandTypeId unitCommandTypeId);
    void CancelAction();
  }
}
