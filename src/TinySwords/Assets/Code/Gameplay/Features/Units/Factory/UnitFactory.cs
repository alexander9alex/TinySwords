using System;
using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.ControlAction.Data;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Factory
{
  class UnitFactory : IUnitFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IIdentifierService _identifiers;
    private readonly IUnitAI _unitUI;

    public UnitFactory(IStaticDataService staticDataService, IIdentifierService identifiers, IUnitAI unitUI)
    {
      _staticDataService = staticDataService;
      _identifiers = identifiers;
      _unitUI = unitUI;
    }

    public void CreateUnit(UnitTypeId type, TeamColor color, Vector3 pos)
    {
      UnitConfig config = _staticDataService.GetUnitConfig(type, color);

      GameEntity unit = CreateEntity.Empty()
          .AddId(_identifiers.Next())
          .AddViewPrefab(config.UnitPrefab)
          .AddWorldPosition(pos)
          .AddMoveDirection(Vector2.zero)
          .AddTeamColor(color)
          .AddAttackReach(config.AttackReach)
          .AddMakeDecisionInterval(config.MakeDecisionInterval)
          .AddMakeDecisionTimer(0)
          .AddCollectTargetsInterval(config.CollectTargetsInterval)
          .AddCollectTargetsTimer(0)
          .AddCollectTargetsRadius(config.CollectTargetRadius)
          .AddTargetBuffer(new List<int>())
          .With(x => x.isUnit = true)
          .With(x => x.isIdle = true)
          .With(x => x.isUpdatePositionAfterSpawning = true)
          .With(x => x.isMovable = true)
        ;

      switch (type)
      {
        case UnitTypeId.Knight:
          CreateKnight(unit);
          break;
        case UnitTypeId.TorchGoblin:
          CreateTorchGoblin(unit);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
    }

    private void CreateKnight(GameEntity unit)
    {
      unit
        .AddUnitAI(_unitUI)
        .AddAllUnitCommandTypeIds(new()
        {
          UnitCommandTypeId.Move,
          UnitCommandTypeId.MoveWithAttack
        })
        .With(x => x.isSelectable = true)
        .With(x => x.isUnselected = true)
        ;
    }

    private void CreateTorchGoblin(GameEntity unit)
    {
      unit.AddUnitAI(_unitUI);
    }
  }
}
