using System.Collections.Generic;
using Code.Gameplay.UtilityAI.Components;

namespace Code.Gameplay.UtilityAI.Brains
{
  public class UnitBrains
  {
    private readonly Convolutions _convolutions;

    private readonly When _when;
    private readonly GetInput _getInput;
    private readonly Score _score;

    public UnitBrains(IBrainsComponents brainsComponents)
    {
      _when = brainsComponents.When;
      _getInput = brainsComponents.GetInput;
      _score = brainsComponents.Score;

      _convolutions = new()
      {
        { _when.DecisionIsStay, _getInput.HasEndDestination, _score.IfFalseThen(+10), "Has not End Destination" },

        { _when.DecisionIsMove, _getInput.UserCommandIsMove, _score.IfTrueThen(+1000), "User Command is Move" },
        { _when.DecisionIsMove, _getInput.HasEndDestination, _score.IfTrueThen(+15), "Has End Destination" },

        { _when.DecisionIsMoveToTarget, _getInput.HasTargets, _score.IfTrueThen(+20), "Has Targets" },
        { _when.DecisionIsMoveToTarget, _getInput.PercentageDistanceToTarget, _score.ScaledByReversed(50), "Move to Nearest Target" },

        { _when.DecisionIsMoveToAllyTarget, _getInput.HasTargets, _score.IfFalseThen(+20), "Has not own Targets" },

        { _when.DecisionIsMoveToAimedTarget, _getInput.CanReachToTarget, _score.IfFalseThen(+1000), "Can not Reach to Target" },

        { _when.DecisionIsAttack, _getInput.CanReachToTarget, _score.IfTrueThen(+100), "Can Reach to Target" },
        { _when.DecisionIsAttack, _getInput.PercentageDistanceToReachedTarget, _score.ScaledByReversed(50), "Attack Nearest Target" },

        { _when.DecisionIsAttackAimedTarget, _getInput.CanReachToTarget, _score.IfTrueThen(+1500), "Can Reach to Target" },
      };
    }

    public IEnumerable<IUtilityFunction> GetUtilityFunctions() =>
      _convolutions;
  }
}
