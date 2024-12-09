using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI.Components
{
  public class When
  {
    public bool DecisionIsStay(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.Stay;

    public bool DecisionIsMove(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.Move;

    public bool DecisionIsMoveToTarget(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.MoveToTarget;

    public bool DecisionIsMoveToAllyTarget(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.MoveToAllyTarget;

    public bool DecisionIsMoveToAimedTarget(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.MoveToAimedTarget;

    public bool DecisionIsAttack(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.Attack;

    public bool DecisionIsAttackAimedTarget(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.AttackAimedTarget;
  }
}
