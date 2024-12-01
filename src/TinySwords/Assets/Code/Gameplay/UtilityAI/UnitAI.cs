using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using Code.Gameplay.UtilityAI.Data;
using UnityEngine;

namespace Code.Gameplay.UtilityAI
{
  class UnitAI : IUnitAI
  {
    private readonly IEnumerable<IUtilityFunction> _utilityFunctions;
    private readonly GameContext _gameContext;

    public UnitAI(UnitBrains unitBrains, GameContext gameContext)
    {
      _utilityFunctions = unitBrains.GetUtilityFunctions();
      _gameContext = gameContext;
    }

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

      foreach (UnitDecision moveToTargetDecision in MoveToTargetDecisions(unit))
        yield return moveToTargetDecision;

      foreach (UnitDecision moveToAllyTargetDecision in MoveToAlliesTargetDecisions(unit))
        yield return moveToAllyTargetDecision;

      foreach (UnitDecision unitDecision in AttackTargetDecisions(unit))
        yield return unitDecision;
    }

    private float? CalculateScore(GameEntity unit, UnitDecision decision)
    {
      IEnumerable<ScoreFactor> scores = (
          from utilityFunction in _utilityFunctions
          where utilityFunction.AppliesTo(unit, decision)
          let input = utilityFunction.GetInput(unit, decision)
          let score = utilityFunction.Score(unit, input)
          select new ScoreFactor(utilityFunction.Name, score)
        );

      return scores.Select(x => x.Score).SumOrNull();
    }

    private IEnumerable<UnitDecision> MoveToTargetDecisions(GameEntity unit)
    {
      if (!unit.hasTargetBuffer)
        yield break;

      foreach (int targetId in unit.TargetBuffer)
      {
        GameEntity target = _gameContext.GetEntityWithId(targetId);

        if (target is { isAlive: true })
          yield return MoveToTargetDecision(target);
      }
    }

    private IEnumerable<UnitDecision> MoveToAlliesTargetDecisions(GameEntity unit)
    {
      if (!unit.hasAllyBuffer)
        yield break;

      foreach (int allyId in unit.AllyBuffer)
      {
        GameEntity ally = _gameContext.GetEntityWithId(allyId);

        if (ally is not { isAlive: true, hasTargetBuffer: true })
          continue;

        foreach (UnitDecision moveToAllyTargetDecision in MoveToAllyTargetDecisions(ally))
          yield return moveToAllyTargetDecision;
      }
    }

    private IEnumerable<UnitDecision> MoveToAllyTargetDecisions(GameEntity ally)
    {
      if (ally.TargetBuffer.Count > 0)
      {
        foreach (UnitDecision moveToAllyTargetDecision in MoveToAllyTargetWhenHasTarget(ally))
          yield return moveToAllyTargetDecision;
        
        yield break;
      }
      
      if (ally.hasAllyTargetId)
        foreach (UnitDecision moveToAllyTargetDecision in MoveToAllyTargetWhenAllyHasNotTarget(ally))
          yield return moveToAllyTargetDecision;
    }

    private IEnumerable<UnitDecision> MoveToAllyTargetWhenAllyHasNotTarget(GameEntity ally)
    {
      GameEntity target = _gameContext.GetEntityWithId(ally.AllyTargetId);

      if (target is { isAlive: true })
        yield return MoveToAllyTargetDecision(target);
    }

    private IEnumerable<UnitDecision> MoveToAllyTargetWhenHasTarget(GameEntity ally)
    {
      foreach (int targetId in ally.TargetBuffer)
      {
        GameEntity target = _gameContext.GetEntityWithId(targetId);

        if (target is { isAlive: true })
          yield return MoveToAllyTargetDecision(target);
      }
    }

    private IEnumerable<UnitDecision> AttackTargetDecisions(GameEntity unit)
    {
      if (!unit.hasReachedTargetBuffer)
        yield break;

      foreach (int reachedTargetId in unit.ReachedTargetBuffer)
      {
        GameEntity target = _gameContext.GetEntityWithId(reachedTargetId);

        if (target is { isAlive: true })
          yield return AttackTargetDecision(reachedTargetId);
      }
    }

    private UnitDecision StayDecision(GameEntity unit)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.Stay,
        Destination = unit.WorldPosition
      };
    }

    private UnitDecision MoveToEndDestinationDecision(GameEntity unit)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.MoveToEndDestination,
        Destination = unit.EndDestination
      };
    }

    private UnitDecision MoveToTargetDecision(GameEntity target)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.MoveToTarget,
        Destination = target.WorldPosition
      };
    }

    private UnitDecision MoveToAllyTargetDecision(GameEntity target)
    {
      // return new UnitDecision
      // {
      // UnitDecisionTypeId = UnitDecisionTypeId.MoveToAllyTarget,
      // Destination = ally.Destination
      // };

      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.MoveToAllyTarget,
        Destination = target.WorldPosition
      };
    }

    private UnitDecision AttackTargetDecision(int targetId)
    {
      GameEntity target = _gameContext.GetEntityWithId(targetId);

      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.Attack,
        Destination = target.WorldPosition,
        TargetId = targetId
      };
    }

    private void PrintDecision(UnitDecision decision, IEnumerable<ScoreFactor> scores)
    {
      Debug.Log("----------------");

      Debug.Log($"Decision: {decision.UnitDecisionTypeId} == {scores.Select(x => x.Score).SumOrNull()}");

      foreach (ScoreFactor scoreFactor in scores)
        Debug.Log(scoreFactor.ToString());
    }
  }
}
