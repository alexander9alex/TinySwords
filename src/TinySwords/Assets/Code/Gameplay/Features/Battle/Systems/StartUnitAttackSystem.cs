using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Effects.Data;
using Entitas;
using ModestTree;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class StartUnitAttackSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;

    private readonly IGroup<GameEntity> _units;
    private readonly List<GameEntity> _buffer = new(4);

    public StartUnitAttackSystem(GameContext game, IPhysicsService physicsService)
    {
      _physicsService = physicsService;

      _units = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Unit,
          GameMatcher.AttackRequest,
          GameMatcher.AttackTarget,
          GameMatcher.AttackReach,
          GameMatcher.Damage,
          GameMatcher.WorldPosition,
          GameMatcher.LookDirection,
          GameMatcher.Available
        ));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        if (unit.isCanAttack)
          Attack(unit);

        unit.isAttackRequest = false;
      }
    }

    private void Attack(GameEntity unit)
    {
      List<int> targets = GetTargets(unit);

      if (targets.IsEmpty())
        return;

      if (targets.Contains(unit.AttackTarget))
        Attack(unit, unit.AttackTarget);
      else
        Attack(unit, targets.First());
    }

    private static void Attack(GameEntity unit, int targetId)
    {
      CreateEntity.Empty()
        .AddTargetId(targetId)
        .AddEffectTypeId(EffectTypeId.Damage)
        .AddEffectValue(unit.Damage);
    }

    private List<int> GetTargets(GameEntity unit)
    {
      return _physicsService.CircleCast(
          unit.WorldPosition.ToVector2() + unit.LookDirection * unit.AttackReach,
          unit.AttackReach,
          GameConstants.UnitsAndBuildingsLayerMask)
        .Select(entity => entity.Id)
        .ToList();
    }
  }
}
