using System.Collections.Generic;
using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class TickToAttackCooldownSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public TickToAttackCooldownSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CanAttack, GameMatcher.AttackCooldown, GameMatcher.NotAttacking)
        .NoneOf(GameMatcher.CanAttackNow));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.ReplaceAttackCooldown(entity.AttackCooldown - _time.DeltaTime);

        if (entity.AttackCooldown <= 0)
        {
          entity.isCanAttackNow = true;
          entity.ReplaceUpdateFieldOfVisionTimer(0);
        }
      }
    }
  }
}
