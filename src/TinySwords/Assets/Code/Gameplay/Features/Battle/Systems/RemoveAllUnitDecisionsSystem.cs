using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class RemoveAllUnitDecisionsSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _units;
    private readonly List<GameEntity> _buffer = new(32);

    public RemoveAllUnitDecisionsSystem(GameContext game)
    {
      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.UnitDecision));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        RemoveUnitDecisions(unit);
      }
    }

    private void RemoveUnitDecisions(GameEntity unit)
    {
      if (unit.hasDestination)
        unit.ReplaceDestination(unit.WorldPosition);

      if (unit.hasTargetId)
        unit.RemoveTargetId();
      
      unit.isFollowToTarget = false;
      unit.isAttackRequest = false;
    }
  }
}
