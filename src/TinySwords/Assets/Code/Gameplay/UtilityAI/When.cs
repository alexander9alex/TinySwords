using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public class When
  {
    public bool ActionIsStay(GameEntity unit, UnitDecision decision) =>
    decision.UnitDecisionTypeId == UnitDecisionTypeId.Stay;

    public bool ActionIsMove(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.Move;
  }
}
