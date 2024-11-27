using Code.Gameplay.Features.Units.Data;
using ModestTree;
using UnityEngine;

namespace Code.Gameplay.UtilityAI.Components
{
  public class GetInput
  {
    private const float False = 0;
    private const float True = 1;

    public float HasEndDestination(GameEntity unit, UnitDecision decision) =>
      unit.hasEndDestination ? True : False;

    public float PercentageDistanceToTarget(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasTargetBuffer || unit.TargetBuffer.IsEmpty() || !unit.hasWorldPosition || !unit.hasCollectTargetsRadius)
        return 0;

      return Vector2.Distance(unit.WorldPosition, decision.Destination) / (unit.CollectTargetsRadius + 0.1f);
    }

    public float HasTarget(GameEntity unit, UnitDecision decision) =>
      unit.hasTargetBuffer && !unit.TargetBuffer.IsEmpty() ? True : False;

    public float CanReachToTarget(GameEntity unit, UnitDecision decision) =>
      unit.hasReachedTargetBuffer && !unit.ReachedTargetBuffer.IsEmpty() ? True : False;

    public float PercentageReachToTarget(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasReachedTargetBuffer || unit.ReachedTargetBuffer.IsEmpty() || !unit.hasWorldPosition || !unit.hasAttackReach)
        return 1;

      return Vector2.Distance(unit.WorldPosition, decision.Destination) / (unit.AttackReach + 0.1f);
    }

    public float IsRunAway(GameEntity unit, UnitDecision decision) =>
      unit.isRunAway ? True : False;
  }
}
