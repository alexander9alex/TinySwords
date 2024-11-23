using System;
using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public class UtilityFunction : IUtilityFunction
  {
    public string Name { get; }

    private readonly Func<GameEntity, UnitDecision, bool> _appliesTo;
    private readonly Func<GameEntity, UnitDecision, float> _getInput;
    private readonly Func<float, GameEntity, float> _score;

    public UtilityFunction(
      Func<GameEntity, UnitDecision, bool> appliesTo,
      Func<GameEntity, UnitDecision, float> getInput,
      Func<float, GameEntity, float> score,
      string name)
    {
      Name = name;
      _score = score;
      _getInput = getInput;
      _appliesTo = appliesTo;
    }

    public bool AppliesTo(GameEntity unit, UnitDecision decision) =>
      _appliesTo(unit, decision);

    public float GetInput(GameEntity unit, UnitDecision decision) =>
      _getInput(unit, decision);

    public float Score(GameEntity unit, float input) =>
      _score(input, unit);
  }
}
