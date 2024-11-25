using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class FinishAttackSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly IGroup<GameEntity> _finishAttackRequests;
    private readonly List<GameEntity> _buffer = new(16);

    public FinishAttackSystem(GameContext game)
    {
      _game = game;
      _finishAttackRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FinishAttack, GameMatcher.CasterId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _finishAttackRequests.GetEntities(_buffer))
      {
        GameEntity unit = _game.GetEntityWithId(request.CasterId);

        unit.isAttacking = false;
        
        if (unit.hasAttackInterval)
          unit.ReplaceAttackCooldown(unit.AttackInterval);

        request.isDestructed = true;
      }
    }
  }
}
