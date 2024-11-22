using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public class ScoredAction : UnitAction
  {
    public float Score;

    public ScoredAction(UnitAction action, float score)
    {
      Score = score;
      
      UnitActionTypeId = action.UnitActionTypeId;
      Destination = action.Destination;
    }
  }
}
