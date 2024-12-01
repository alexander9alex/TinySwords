using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI.Components
{
  public class When
  {
    public bool ActionIsStay(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.Stay;

    public bool ActionIsMoveToEndDestination(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.MoveToEndDestination;

    public bool ActionIsMoveToTarget(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.MoveToTarget;

    public bool ActionIsAttack(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.Attack;

    public bool ActionIsMoveToAllyTarget(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.MoveToAllyTarget;
  }
}
