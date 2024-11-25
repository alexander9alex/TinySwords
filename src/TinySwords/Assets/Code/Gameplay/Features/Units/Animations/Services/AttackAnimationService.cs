using Code.Common.Entities;
using Code.Common.Extensions;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Animations.Services
{
  class AttackAnimationService : IAttackAnimationService
  {
    private readonly GameContext _game;

    public AttackAnimationService(GameContext game)
    {
      _game = game;
    }

    public void UnitMakeHit(int unitId)
    {
      CreateEntity.Empty()
        .AddCasterId(unitId)
        .With(x => x.isMakeHit = true);
    }

    public void UnitFinishedAttack(int unitId)
    {
      GameEntity unit = _game.GetEntityWithId(unitId);

      if (!unit.hasAttackCooldown || !unit.hasAttackInterval)
        return;

      unit.isAttacking = false;
      unit.ReplaceAttackCooldown(unit.AttackInterval);

      // Debug.Log($"Unit with id {unitId} finished attack...");
    }
  }
}
