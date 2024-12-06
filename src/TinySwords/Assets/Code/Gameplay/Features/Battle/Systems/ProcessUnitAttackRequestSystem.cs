using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class ProcessUnitAttackRequestSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly IGroup<GameEntity> _attackRequests;
    private readonly List<GameEntity> _buffer = new(16);

    public ProcessUnitAttackRequestSystem(GameContext game)
    {
      _game = game;
      _attackRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AttackRequest, GameMatcher.CasterId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _attackRequests.GetEntities(_buffer))
      {
        GameEntity unit = _game.GetEntityWithId(request.CasterId);

        if (unit is { isAlive: true, isCanAttack: true, isCanAttackNow: true })
        {
          unit.isCanAttackNow = false;
          unit.isNotAttacking = false;
          unit.isAttacking = true;

          unit.isAnimateAttackRequest = true;
        }

        request.isDestructed = true;
      }
    }
  }
}
