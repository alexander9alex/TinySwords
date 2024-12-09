using System;

namespace Code.Gameplay.UtilityAI.Components
{
  public class Score
  {
    public Func<float, GameEntity, float> IfTrueThen(float value) =>
      (ifTrue, _) => value * ifTrue;

    public Func<float, GameEntity, float> IfFalseThen(float value) =>
      (ifFalse, _) => ifFalse == 0 ? value : 0;

    public Func<float, GameEntity, float> ScaledByReversed(float scaled) =>
      (percentageInput, _) => scaled - percentageInput * scaled;
  }
}
