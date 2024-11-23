using System;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Constants;
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
      switch (type)
      {
        case UnitTypeId.Knight:
          CreateKnight(type, color, pos);
          break;
        case UnitTypeId.TorchGoblin:
          CreateTorchGoblin(type, color, pos);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }

    }

    private void CreateKnight(UnitTypeId type, TeamColor color, Vector3 pos)
    {
      UnitConfig config = _staticDataService.GetUnitConfig(type, color);
      
      CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddViewPrefab(config.UnitPrefab)
        .AddWorldPosition(pos)
        .AddMoveDirection(Vector2.zero)
        .AddMakeDecisionInterval(GameConstants.MakeDecisionInterval)
        .AddMakeDecisionTimer(0)
        .AddUnitAI(_unitUI)
        .AddAllUnitCommandTypeIds(new()
        {
          UnitCommandTypeId.Move,
          UnitCommandTypeId.MoveWithAttack
        })
        .With(x => x.isUnit = true)
        .With(x => x.isSelectable = true)
        .With(x => x.isUnselected = true)
        .With(x => x.isIdle = true)
        .With(x => x.isUpdatePositionAfterSpawning = true)
        .With(x => x.isMovable = true)
        ;
    }

    private void CreateTorchGoblin(UnitTypeId type, TeamColor color, Vector3 pos)
    {
      UnitConfig config = _staticDataService.GetUnitConfig(type, color);
      
      CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddViewPrefab(config.UnitPrefab)
        .AddWorldPosition(pos)
        .AddMoveDirection(Vector2.zero)
        .AddMakeDecisionInterval(GameConstants.MakeDecisionInterval)
        .AddMakeDecisionTimer(0)
        .AddUnitAI(_unitUI)
        .With(x => x.isUnit = true)
        .With(x => x.isIdle = true)
        .With(x => x.isUpdatePositionAfterSpawning = true)
        .With(x => x.isMovable = true)
        ;
    }
  }
}
