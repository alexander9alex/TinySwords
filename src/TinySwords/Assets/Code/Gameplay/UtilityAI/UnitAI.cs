using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using Code.Gameplay.UtilityAI.Data;

namespace Code.Gameplay.UtilityAI
{
  class UnitAI : IUnitAI
  {
    private readonly IEnumerable<IUtilityFunction> _utilityFunctions;

    public UnitAI(UnitBrains unitBrains) =>
      _utilityFunctions = unitBrains.GetUtilityFunctions();

    public UnitDecision MakeBestDecision(GameEntity unit)
    {
      IEnumerable<ScoredDecision> decisions = GetScoredUnitDecisions(unit);
      return decisions.FindMax(x => x.Score);
    }

    private IEnumerable<ScoredDecision> GetScoredUnitDecisions(GameEntity unit)
    {
      foreach (UnitDecision decision in GetAvailableDecisions(unit))
      {
        float? score = CalculateScore(unit, decision);

        if (!score.HasValue)
          continue;

        yield return new ScoredDecision(decision, score.Value);
      }
    }

    private IEnumerable<UnitDecision> GetAvailableDecisions(GameEntity unit)
    {
      yield return StayDecision(unit);

      if (unit.hasEndDestination)
        yield return MoveToEndDestinationDecision(unit);

      // todo: if (has target) => attack
      // ...
      // ...
      // ...
    }

    private float? CalculateScore(GameEntity unit, UnitDecision decision)
    {
      IEnumerable<float> scores = (
          from utilityFunction in _utilityFunctions
          where utilityFunction.AppliesTo(unit, decision)
          let input = utilityFunction.GetInput(unit, decision)
          select utilityFunction.Score(unit, input)
        );

      return scores.SumOrNull();
    }

    private UnitDecision StayDecision(GameEntity unit)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.Stay,
      };
    }

    private UnitDecision MoveToEndDestinationDecision(GameEntity unit)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.Move,
        Destination = unit.EndDestination
      };
    }
  }
}
