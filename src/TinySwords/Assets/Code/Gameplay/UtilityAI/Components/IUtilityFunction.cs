using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI.Components
{
  public interface IUtilityFunction
  {
    bool AppliesTo(GameEntity unit, UnitDecision decision);
    float GetInput(GameEntity unit, UnitDecision decision);
    float Score(GameEntity unit, float input);
    string Name { get; }
  }
}
