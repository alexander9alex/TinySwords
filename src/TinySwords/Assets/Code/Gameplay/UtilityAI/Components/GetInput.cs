using Code.Gameplay.Features.Units.Data;
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
      if (!decision.HasTarget || !unit.hasWorldPosition || !unit.hasCollectTargetsRadius)
        return 0;

      return Vector2.Distance(unit.WorldPosition, decision.Position) / unit.CollectTargetsRadius;
    }

    public float HasTarget(GameEntity unit, UnitDecision decision) =>
      decision.HasTarget ? True : False;

    public float PercentageReachToTarget(GameEntity unit, UnitDecision decision)
    {
      if (!decision.HasTarget || !unit.hasWorldPosition || !unit.hasAttackReach)
        return 1;

      float distanceToTarget = Vector2.Distance(unit.WorldPosition, decision.Position);
      
      if (distanceToTarget > unit.AttackReach)
        return 1;

      return distanceToTarget / unit.AttackReach;
    }

    public float CanReachToTarget(GameEntity unit, UnitDecision decision)
    {
      if (!decision.HasTarget || !unit.hasWorldPosition || !unit.hasAttackReach)
        return 0;

      return Vector2.Distance(unit.WorldPosition, decision.Position) <= unit.AttackReach 
        ? True
        : False;
    }
  }
}
