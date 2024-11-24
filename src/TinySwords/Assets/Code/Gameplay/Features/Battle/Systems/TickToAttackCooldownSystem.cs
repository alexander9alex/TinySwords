using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class TickToAttackCooldownSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;

    public TickToAttackCooldownSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher.MakeDecisionTimer);
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.ReplaceAttackCooldown(entity.AttackCooldown - _time.DeltaTime);

        if (entity.AttackCooldown <= 0)
        {
          entity.isCanAttack = true;

          if (entity.hasAttackInterval)
            entity.ReplaceAttackCooldown(entity.AttackInterval);
        }
      }
    }
  }
}
