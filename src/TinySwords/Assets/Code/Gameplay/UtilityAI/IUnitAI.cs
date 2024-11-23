using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public interface IUnitAI
  {
    UnitDecision MakeBestDecision(GameEntity unit);
  }
}
