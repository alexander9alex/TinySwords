using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class ProcessUnitAttackRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(16);

    public ProcessUnitAttackRequestSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AttackRequest, GameMatcher.CanAttackNow, GameMatcher.CanAttack, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.isCanAttackNow = false;
        entity.isNotAttacking = false;
        entity.isAttacking = true;
        
        entity.isAnimateAttackRequest = true;
        entity.isAttackRequest = false;
      }
    }
  }
}
