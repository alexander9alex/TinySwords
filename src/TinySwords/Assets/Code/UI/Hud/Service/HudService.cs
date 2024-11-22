using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.ControlAction.Configs;
using Code.Gameplay.Features.ControlAction.Data;
using UnityEngine;

namespace Code.UI.Hud.Service
{
  class HudService : IHudService
  {
    public event Action UpdateHud;
    public event Action UpdateActionDescription;

    private readonly IStaticDataService _staticData;
    private List<UnitActionUIConfig> _availableUnitCommandUIConfigs = new();
    private GameObject _actionDescription;

    public HudService(IStaticDataService staticData) =>
      _staticData = staticData;

    public void UpdateAvailableActions(List<UnitCommandTypeId> availableCommands)
    {
      if (Equals(_availableUnitCommandUIConfigs.Select(x => x.UnitCommandTypeId), availableCommands))
        return;

      _availableUnitCommandUIConfigs = _staticData.GetUnitCommandUIConfigs(availableCommands);
      UpdateHud?.Invoke();
    }

    public List<UnitActionUIConfig> GetAvailableUnitActionUIConfigs() =>
      _availableUnitCommandUIConfigs;

    public GameObject GetActionDescription() =>
      _actionDescription;

    public void SetAction(UnitCommandTypeId unitCommandTypeId)
    {
      _availableUnitCommandUIConfigs = new();
      UpdateHud?.Invoke();

      _actionDescription = _staticData.GetUnitCommandUIConfig(unitCommandTypeId).UnitActionDescriptionPrefab;
      UpdateActionDescription?.Invoke();
    }

    public void CancelAction()
    {
      _actionDescription = null;
      UpdateActionDescription?.Invoke();
    }

    public void ClickedToButton(UnitCommandTypeId unitCommandTypeId)
    {
      GameEntity entity = CreateEntity.Empty();

      switch (unitCommandTypeId)
      {
        case UnitCommandTypeId.Move:
          entity
            .AddUnitCommandTypeId(UnitCommandTypeId.Move)
            .With(x => x.isMoveControlAction = true);

          break;
        case UnitCommandTypeId.MoveWithAttack:
          entity
            .AddUnitCommandTypeId(UnitCommandTypeId.MoveWithAttack)
            .With(x => x.isMoveWithAttackControlAction = true);

          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(unitCommandTypeId), unitCommandTypeId, null);
      }
    }
  }
}
