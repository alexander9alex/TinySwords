using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI.Components
{
  public class When
  {
    public bool ActionIsStay(GameEntity unit, UnitDecision decision) =>
    decision.UnitDecisionTypeId == UnitDecisionTypeId.Stay;

    public bool ActionIsMove(GameEntity unit, UnitDecision decision) =>
      decision.UnitDecisionTypeId == UnitDecisionTypeId.Move;
  }
}
