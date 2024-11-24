using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI.Data
{
  public class ScoredDecision : UnitDecision
  {
    public float Score;

    public ScoredDecision(UnitDecision decision, float score)
    {
      Score = score;
      
      UnitDecisionTypeId = decision.UnitDecisionTypeId;
      Position = decision.Position;
    }
  }
}
