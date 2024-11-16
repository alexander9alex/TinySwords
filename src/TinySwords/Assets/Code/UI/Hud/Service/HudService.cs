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

    public void UpdateAvailableActions(List<ControlActionTypeId> availableActions)
    {
      if (Equals(_availableButtonConfigs.Select(x => x.ControlActionTypeId), availableActions))
        return;

      _availableButtonConfigs = _staticData.GetControlButtonConfigs(availableActions);
      UpdateHud?.Invoke();
    }

    public List<ControlButtonConfig> GetAvailableButtonConfigs() =>
      _availableButtonConfigs;

    public GameObject GetActionDescription() =>
      _actionDescription;

    public void SetAction(ControlActionTypeId controlActionTypeId)
    {
      _availableButtonConfigs = new();
      UpdateHud?.Invoke();

      _actionDescription = _staticData.GetActionDescription(controlActionTypeId);
      UpdateActionDescription?.Invoke();
    }

    public void CancelAction()
    {
      _actionDescription = null;
      UpdateActionDescription?.Invoke();
    }

    public void ClickedToButton(ControlActionTypeId controlActionTypeId)
    {
      GameEntity entity = CreateEntity.Empty();

      switch (controlActionTypeId)
      {
        case ControlActionTypeId.Move:
          entity
            .AddActionTypeId(ControlActionTypeId.Move)
            .With(x => x.isMoveAction = true);

          break;
        case ControlActionTypeId.MoveWithAttack:
          entity
            .AddActionTypeId(ControlActionTypeId.MoveWithAttack)
            .With(x => x.isMoveWithAttackAction = true);

          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(controlActionTypeId), controlActionTypeId, null);
      }
    }
  }
}
