using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  class UnitAI : IUnitAI
  {
    private readonly IEnumerable<IUtilityFunction> _utilityFunctions;

    public UnitAI(UnitBrains unitBrains)
    {
      _utilityFunctions = unitBrains.GetUtilityFunctions();
    }
    
    public UnitAction MakeBestDecision(GameEntity unit)
    {
      IEnumerable<ScoredAction> actions = GetScoredUnitActions(unit);
      return actions.FindMax(x => x.Score);
    }

    private IEnumerable<ScoredAction> GetScoredUnitActions(GameEntity unit)
    {
      foreach (UnitAction action in GetAvailableActions(unit))
      {
        float? score = CalculateScore(unit, action);

        if (!score.HasValue)
          continue;

        yield return new ScoredAction(action, score.Value);
      }
    }

    private IEnumerable<UnitAction> GetAvailableActions(GameEntity unit)
    {
      yield return StayAction(unit);

      if (unit.hasDestination)
        yield return MoveToEndDestinationAction(unit);

      // todo: if (has target) => attack
      // ...
      // ...
      // ...
    }

    private float? CalculateScore(GameEntity unit, UnitAction action)
    {
      IEnumerable<float> scores = (
          from utilityFunction in _utilityFunctions
          where utilityFunction.AppliesTo(unit, action)
          let input = utilityFunction.GetInput(unit, action)
          select utilityFunction.Score(unit, input)
        );

      return scores.SumOrNull();
    }

    private UnitAction StayAction(GameEntity unit)
    {
      return new UnitAction
      {
        UnitActionTypeId = UnitActionTypeId.Stay,
      };
    }

    private UnitAction MoveToEndDestinationAction(GameEntity unit)
    {
      return new UnitAction
      {
        UnitActionTypeId = UnitActionTypeId.Move,
        Destination = unit.Destination
      };
    }
  }
}
