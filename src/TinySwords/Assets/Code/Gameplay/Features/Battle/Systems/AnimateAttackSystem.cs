using System.Collections.Generic;
using Entitas;

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
        .AllOf(GameMatcher.Unit, GameMatcher.AnimateAttack, GameMatcher.AttackAnimator, GameMatcher.TargetId));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        GameEntity target = _game.GetEntityWithId(unit.TargetId);

        if (target.hasWorldPosition)
        {
          unit.AttackAnimator.AnimateAttack((target.WorldPosition - unit.WorldPosition).normalized);
          unit.isAttacking = true;
        }

        unit.isAnimateAttack = false;
      }
    }
  }
}
