using System;
using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Features.Units.Markers;
using Code.Gameplay.Features.Win.Markers;
using Code.Gameplay.UtilityAI;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Factory
{
  public class UnitFactory : IUnitFactory
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

    public GameEntity CreateUnit(UnitTypeId type, TeamColor color, Vector3 pos)
    {
      UnitConfig config = _staticDataService.GetUnitConfig(type, color);

      GameEntity unit = CreateEntity.Empty()
          .AddId(_identifiers.Next())
          .AddViewPrefab(config.UnitPrefab)
          .AddWorldPosition(pos)
          .AddMoveDirection(Vector2.zero)
          .AddLookDirection(Vector2.zero)
          .AddTeamColor(color)
          .AddSpeed(config.Speed)
          .AddDamage(config.Damage)
          .AddCurrentHp(config.Hp)
          .AddMaxHp(config.Hp)
          .AddUnitAI(_unitUI)
          .AddUnitTypeId(type)
          .AddAttackCooldown(0)
          .AddAttackInterval(config.AttackCooldown)
          .AddAttackReach(config.AttackReach)
          .AddVisibleEntityBuffer(new())
          .AddVisionRadius(config.VisionRadius)
          .AddUpdateFieldOfVisionTimer(0)
          .AddUpdateFieldOfVisionInterval(config.UpdateFieldOfVisionInterval)
          .AddTimeSinceLastVisionUpdated(0)
          .AddTargetBuffer(new List<int>())
          .AddCollectTargetsRadius(config.CollectTargetRadius)
          .AddReachedTargetBuffer(new List<int>())
          .AddCollectReachedTargetsRadius(config.AttackReach)
          .AddAllyBuffer(new List<int>())
          .AddCollectAlliesRadius(config.CollectAlliesRadius)
          .With(x => x.isUnit = true)
          .With(x => x.isIdle = true)
          .With(x => x.isInitializationRequest = true)
          .With(x => x.isMovable = true)
          .With(x => x.isCanAttack = true)
          .With(x => x.isNotAttacking = true)
          .With(x => x.isCanAttackNow = true)
          .With(x => x.isAlive = true)
          .With(x => x.isFocusing = true)
          .With(x => x.isUnfocused = true)
        ;

      switch (type, color)
      {
        case (UnitTypeId.Knight, TeamColor.Blue):
          CreateBlueKnight(unit);
          break;
        case (UnitTypeId.Knight, TeamColor.White):
          CreateWhiteKnight(unit);
          break;
        case (UnitTypeId.TorchGoblin, TeamColor.Red):
          CreateRedTorchGoblin(unit);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }

      return unit;
    }

    public GameEntity CreateUnit(UnitMarker unitMarker)
    {
      GameEntity unit = CreateUnit(unitMarker.UnitTypeId, unitMarker.Color, unitMarker.transform.position);

      if (unitMarker.GetComponent<KillToWinMarker>())
        unit.isKillToWin = true;

      return unit;
    }

    private void CreateBlueKnight(GameEntity unit)
    {
      unit
        .AddAllUnitCommandTypeIds(new()
        {
          CommandTypeId.Move,
          CommandTypeId.MoveWithAttack,
          CommandTypeId.AimedAttack,
        })
        .With(x => x.isSelectable = true)
        .With(x => x.isUnselected = true)
        .With(x => x.isGlowing = true)
        .With(x => x.isBlueTeamColor = true)
        ;
    }

    private void CreateWhiteKnight(GameEntity unit)
    {
      unit
        .With(x => x.isNeutralUnit = true)
        .With(x => x.isWhiteTeamColor = true)
        ;
    }

    private void CreateRedTorchGoblin(GameEntity unit)
    {
      unit
        .With(x => x.isRedTeamColor = true);
    }
  }
}
