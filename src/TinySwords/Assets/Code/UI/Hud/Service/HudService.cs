using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.UI.Buttons.Configs;
using Code.UI.Buttons.Data;
using UnityEngine;

namespace Code.UI.Hud.Service
{
  class HudService : IHudService
  {
    public event Action UpdateHud;
    public event Action UpdateActionDescription;

    private readonly IStaticDataService _staticData;
    private List<ControlButtonConfig> _availableButtonConfigs = new();
    private GameObject _actionDescription;

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

    public GameObject GetActionDescription() =>
      _actionDescription;

    public void SetAction(ActionTypeId actionTypeId)
    {
      _availableButtonConfigs = new();
      UpdateHud?.Invoke();

      _actionDescription = _staticData.GetActionDescription(actionTypeId);
      UpdateActionDescription?.Invoke();
    }

    public void CancelAction()
    {
      _actionDescription = null;
      UpdateActionDescription?.Invoke();
    }

    public void ClickedToButton(ActionTypeId actionTypeId)
    {
      GameEntity entity = CreateEntity.Empty();

      switch (actionTypeId)
      {
        case ActionTypeId.Move:
          entity
            .AddActionTypeId(ActionTypeId.Move)
            .With(x => x.isMoveAction = true);

          break;
        case ActionTypeId.MoveWithAttack:
          entity
            .AddActionTypeId(ActionTypeId.MoveWithAttack)
            .With(x => x.isMoveWithAttackAction = true);

          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(actionTypeId), actionTypeId, null);
      }
    }
  }
}
