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
        { When.ActionIsStay, GetInput.HasEndDestination, Score.IfFalseThen(+50), "Stay here" },
        
        { When.ActionIsMove, GetInput.HasEndDestination, Score.IfTrueThen(+50), "Move to Destination" },
        
        // if target is unit => +100
        // if target is building => +50
      };
    }

    public IEnumerable<IUtilityFunction> GetUtilityFunctions() =>
      _convolutions;
  }
}
