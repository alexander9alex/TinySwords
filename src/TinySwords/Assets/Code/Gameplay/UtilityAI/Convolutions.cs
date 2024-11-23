using System;
using System.Collections.Generic;
using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public class Convolutions : List<UtilityFunction>
  {
    public void Add(
      Func<GameEntity, UnitDecision, bool> appliesTo,
      Func<GameEntity, UnitDecision, float> getInput,
      Func<float, GameEntity, float> score,
      string name)
    {
      Add(new UtilityFunction(appliesTo, getInput, score, name));
    }
  }
}
