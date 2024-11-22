using System;
using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public class UtilityFunction : IUtilityFunction
  {
    public string Name { get; }

    private readonly Func<GameEntity, UnitAction, bool> _appliesTo;
    private readonly Func<GameEntity, UnitAction, float> _getInput;
    private readonly Func<float, GameEntity, float> _score;

    public UtilityFunction(
      Func<GameEntity, UnitAction, bool> appliesTo,
      Func<GameEntity, UnitAction, float> getInput,
      Func<float, GameEntity, float> score,
      string name)
    {
      Name = name;
      _score = score;
      _getInput = getInput;
      _appliesTo = appliesTo;
    }

    public bool AppliesTo(GameEntity unit, UnitAction action) =>
      _appliesTo(unit, action);

    public float GetInput(GameEntity unit, UnitAction action) =>
      _getInput(unit, action);

    public float Score(GameEntity unit, float input) =>
      _score(input, unit);
  }
}
