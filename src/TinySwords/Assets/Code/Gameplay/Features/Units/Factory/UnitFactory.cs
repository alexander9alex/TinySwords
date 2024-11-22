using System;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.ControlAction.Data;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Factory
{
  class UnitFactory : IUnitFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IIdentifierService _identifiers;

    public UnitFactory(IStaticDataService staticDataService, IIdentifierService identifiers)
    {
      _staticDataService = staticDataService;
      _identifiers = identifiers;
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
        .AddMakeDecisionInterval(0.5f)
        .AddMakeDecisionTimer(0.5f)
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
        .AddMakeDecisionInterval(0.5f)
        .AddMakeDecisionTimer(0.5f)
        .With(x => x.isUnit = true)
        .With(x => x.isIdle = true)
        .With(x => x.isUpdatePositionAfterSpawning = true)
        .With(x => x.isMovable = true)
        ;
    }
  }
}
