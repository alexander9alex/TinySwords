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
        { When.DecisionIsStay, GetInput.HasEndDestination, Score.IfFalseThen(+10), "Has not End Destination" },
        
        { When.DecisionIsMove, GetInput.UserCommandIsMove, Score.IfTrueThen(+1000), "User Command is Move" },
        { When.DecisionIsMove, GetInput.HasEndDestination, Score.IfTrueThen(+15), "Has End Destination" },
        
        { When.DecisionIsMoveToTarget, GetInput.HasTargets, Score.IfTrueThen(+20), "Has Targets" },
        { When.DecisionIsMoveToTarget, GetInput.PercentageDistanceToTarget, Score.ScaledByReversed(50), "Move to Nearest Target" },
        
        { When.DecisionIsMoveToAllyTarget, GetInput.HasTargets, Score.IfFalseThen(+20), "Has not own Targets" },
        
        { When.DecisionIsMoveToAimedTarget, GetInput.CanReachToTarget, Score.IfFalseThen(+1000), "Can not Reach to Target" },
        
        { When.DecisionIsAttack, GetInput.CanReachToTarget, Score.IfTrueThen(+100), "Can Reach to Target" },
        { When.DecisionIsAttack, GetInput.PercentageDistanceToReachedTarget, Score.ScaledByReversed(50), "Attack Nearest Target" },
        
        { When.DecisionIsAttackAimedTarget, GetInput.CanReachToTarget, Score.IfTrueThen(+1500), "Can Reach to Target" },
        
        // todo if target is unit => +100
        // todo if target is building => +50
      };
    }

    public IEnumerable<IUtilityFunction> GetUtilityFunctions() =>
      _convolutions;
  }
}
