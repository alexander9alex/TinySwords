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
    private List<UnitActionUIConfig> _availableUnitActionUIConfigs = new();
    private GameObject _actionDescription;

    public HudService(IStaticDataService staticData) =>
      _staticData = staticData;

    public void UpdateAvailableActions(List<UnitActionTypeId> availableActions)
    {
      if (Equals(_availableUnitActionUIConfigs.Select(x => x.UnitActionTypeId), availableActions))
        return;

      _availableUnitActionUIConfigs = _staticData.GetUnitActionUIConfigs(availableActions);
      UpdateHud?.Invoke();
    }

    public List<UnitActionUIConfig> GetAvailableUnitActionUIConfigs() =>
      _availableUnitActionUIConfigs;

    public GameObject GetActionDescription() =>
      _actionDescription;

    public void SetAction(UnitActionTypeId unitActionTypeId)
    {
      _availableUnitActionUIConfigs = new();
      UpdateHud?.Invoke();

      _actionDescription = _staticData.GetUnitActionUIConfig(unitActionTypeId).UnitActionDescriptionPrefab;
      UpdateActionDescription?.Invoke();
    }

    public void CancelAction()
    {
      _actionDescription = null;
      UpdateActionDescription?.Invoke();
    }

    public void ClickedToButton(UnitActionTypeId unitActionTypeId)
    {
      GameEntity entity = CreateEntity.Empty();

      switch (unitActionTypeId)
      {
        case UnitActionTypeId.Move:
          entity
            .AddUnitActionTypeId(UnitActionTypeId.Move)
            .With(x => x.isMoveControlAction = true);

          break;
        case UnitActionTypeId.MoveWithAttack:
          entity
            .AddUnitActionTypeId(UnitActionTypeId.MoveWithAttack)
            .With(x => x.isMoveWithAttackControlAction = true);

          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(unitActionTypeId), unitActionTypeId, null);
      }
    }
  }
}
