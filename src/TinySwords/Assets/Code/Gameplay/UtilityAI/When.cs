using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public class When
  {
    public bool ActionIsStay(GameEntity unit, UnitAction action) =>
    action.UnitActionTypeId == UnitActionTypeId.Stay;

    public bool ActionIsMove(GameEntity unit, UnitAction action) =>
      action.UnitActionTypeId == UnitActionTypeId.Move;
  }
}
