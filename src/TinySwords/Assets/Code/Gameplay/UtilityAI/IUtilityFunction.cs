using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public interface IUtilityFunction
  {
    bool AppliesTo(GameEntity unit, UnitAction action);
    float GetInput(GameEntity unit, UnitAction action);
    float Score(GameEntity unit, float input);
    string Name { get; }
  }
}
