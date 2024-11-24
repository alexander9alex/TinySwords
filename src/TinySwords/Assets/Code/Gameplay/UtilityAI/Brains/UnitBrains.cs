using System.Collections.Generic;
using Code.Gameplay.UtilityAI.Components;

namespace Code.Gameplay.UtilityAI.Brains
{
  public class UnitBrains
  {
    private readonly Convolutions _convolutions;

    private readonly When When;
    private readonly GetInput GetInput;
    private readonly Score Score;

    public UnitBrains(IBrainsComponents brainsComponents)
    {
      When = brainsComponents.When;
      GetInput = brainsComponents.GetInput;
      Score = brainsComponents.Score;

      _convolutions = new()
      {
        { When.ActionIsStay, GetInput.HasEndDestination, Score.IfFalseThen(+10), "Has not End Destination" },
        
        { When.ActionIsMoveToEndDestination, GetInput.HasEndDestination, Score.IfTrueThen(+10), "Has End Destination" },
        { When.ActionIsMoveToEndDestination, GetInput.HasTarget, Score.IfFalseThen(+30), "Has Not Target" },
        
        { When.ActionIsMoveToTarget, GetInput.HasTarget, Score.IfTrueThen(+20), "Has Target" },
        { When.ActionIsMoveToTarget, GetInput.PercentageDistanceToTarget, Score.ScaledByReversed(50), "Move to Nearest Target" },
        { When.ActionIsMoveToTarget, GetInput.CanReachToTarget, Score.IfFalseThen(+20), "Can Reach to Target" },
        
        { When.ActionIsAttack, GetInput.HasTarget, Score.IfTrueThen(+20), "Has Target" },
        { When.ActionIsAttack, GetInput.PercentageDistanceToTarget, Score.ScaledByReversed(50), "Distance to Target" },
        { When.ActionIsAttack, GetInput.CanReachToTarget, Score.IfTrueThen(+20), "Can Reach to Target" },
        { When.ActionIsAttack, GetInput.PercentageReachToTarget, Score.ScaledByReversed(50), "Attack Nearest Target" },
        
        // if target is unit => +100
        // if target is building => +50
      };
    }

    public IEnumerable<IUtilityFunction> GetUtilityFunctions() =>
      _convolutions;
  }
}
