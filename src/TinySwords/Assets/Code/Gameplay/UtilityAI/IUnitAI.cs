using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public interface IUnitAI
  {
    UnitAction MakeBestDecision(GameEntity unit);
  }
}
