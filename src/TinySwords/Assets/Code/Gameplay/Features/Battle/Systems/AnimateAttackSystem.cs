using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class AnimateAttackSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly IGroup<GameEntity> _units;
    private readonly List<GameEntity> _buffer = new(4);

    public AnimateAttackSystem(GameContext game)
    {
      _game = game;
      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CanAttack, GameMatcher.AnimateAttackRequest, GameMatcher.AttackAnimator, GameMatcher.TargetId));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        GameEntity target = _game.GetEntityWithId(unit.TargetId);

        if (target is { hasWorldPosition: true })
        {
          Vector3 attackDirection = (target.WorldPosition - unit.WorldPosition).normalized;
          unit.AttackAnimator.AnimateAttack(attackDirection);
          unit.ReplaceAttackDirection(attackDirection);
        }
        unit.isAnimateAttackRequest = false;
      }
    }
  }
}
