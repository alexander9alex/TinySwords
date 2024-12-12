using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using Code.Gameplay.UtilityAI.Data;
using UnityEngine;

namespace Code.Gameplay.UtilityAI
{
  public class UnitAI : IUnitAI
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
      yield return StayDecision();

      foreach (UnitDecision decision in MoveDecisions(unit))
        yield return decision;

      foreach (UnitDecision decision in MoveToTargetDecisions(unit))
        yield return decision;

      foreach (UnitDecision decision in MoveToAlliesTargetDecisions(unit))
        yield return decision;

      foreach (UnitDecision decision in MoveToAimedTargetDecisions(unit))
        yield return decision;

      foreach (UnitDecision decision in AttackTargetDecisions(unit))
        yield return decision;

      foreach (UnitDecision decision in AttackAimedTargetDecisions(unit))
        yield return decision;
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

    private IEnumerable<UnitDecision> MoveDecisions(GameEntity unit)
    {
      if (!unit.hasUserCommand || !unit.UserCommand.WorldPosition.HasValue)
        yield break;

      if (unit.UserCommand.CommandTypeId is CommandTypeId.Move or CommandTypeId.MoveWithAttack)
        yield return MoveDecision(unit);
    }

    private IEnumerable<UnitDecision> MoveToTargetDecisions(GameEntity unit)
    {
      if (!unit.hasTargetBuffer || unit.TargetBuffer.Count == 0)
        yield break;

      GameEntity nearestTarget = unit.TargetBuffer
        .Select(targetId => _gameContext.GetEntityWithId(targetId))
        .OrderBy(target => Vector2.Distance(unit.WorldPosition, target.WorldPosition))
        .First(target => target.isAlive);

      yield return MoveToTargetDecision(nearestTarget);
    }

    private IEnumerable<UnitDecision> MoveToAlliesTargetDecisions(GameEntity unit)
    {
      if (!unit.hasAllyBuffer || !unit.hasTargetBuffer || unit.TargetBuffer.Count > 0)
        yield break;

      foreach (int allyId in unit.AllyBuffer)
      {
        GameEntity ally = _gameContext.GetEntityWithId(allyId);

        if (ally is not { isAlive: true })
          continue;

        foreach (UnitDecision moveToAllyTargetDecision in MoveToAllyTargetDecisions(ally))
          yield return moveToAllyTargetDecision;
      }
    }

    private IEnumerable<UnitDecision> MoveToAllyTargetDecisions(GameEntity ally)
    {
      if (ally.hasTargetBuffer && ally.TargetBuffer.Count > 0)
      {
        foreach (int targetId in ally.TargetBuffer)
        {
          GameEntity target = _gameContext.GetEntityWithId(targetId);

          if (target is not { isAlive: true })
            continue;

          yield return MoveToAllyTargetDecision(target);
          yield break;
        }
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

    private IEnumerable<UnitDecision> MoveToAimedTargetDecisions(GameEntity unit)
    {
      if (!unit.hasUserCommand || unit.UserCommand.CommandTypeId != CommandTypeId.AimedAttack || !unit.UserCommand.TargetId.HasValue)
        yield break;

      GameEntity aimedTarget = _gameContext.GetEntityWithId(unit.UserCommand.TargetId.Value);

      if (aimedTarget is { isAlive: true })
        yield return MoveToAimedTargetDecision(aimedTarget);
    }

    private IEnumerable<UnitDecision> AttackTargetDecisions(GameEntity unit)
    {
      if (!unit.hasReachedTargetBuffer)
        yield break;

      foreach (int targetId in unit.ReachedTargetBuffer)
      {
        GameEntity target = _gameContext.GetEntityWithId(targetId);

        if (target is { isAlive: true })
          yield return AttackTargetDecision(target);
      }
    }

    private IEnumerable<UnitDecision> AttackAimedTargetDecisions(GameEntity unit)
    {
      if (!unit.hasUserCommand || unit.UserCommand.CommandTypeId != CommandTypeId.AimedAttack || !unit.UserCommand.TargetId.HasValue)
        yield break;

      GameEntity aimedTarget = _gameContext.GetEntityWithId(unit.UserCommand.TargetId.Value);

      if (aimedTarget is { isAlive: true })
        yield return AttackAimedTargetDecision(aimedTarget);
    }

    private UnitDecision StayDecision()
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.Stay
      };
    }

    private UnitDecision MoveDecision(GameEntity unit)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.Move,
        Destination = unit.UserCommand.WorldPosition.Value
      };
    }

    private UnitDecision MoveToTargetDecision(GameEntity target)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.MoveToTarget,
        TargetId = target.Id
      };
    }

    private UnitDecision MoveToAllyTargetDecision(GameEntity target)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.MoveToAllyTarget,
        TargetId = target.Id
      };
    }

    private UnitDecision MoveToAimedTargetDecision(GameEntity target)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.MoveToAimedTarget,
        TargetId = target.Id
      };
    }

    private UnitDecision AttackTargetDecision(GameEntity target)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.Attack,
        TargetId = target.Id
      };
    }

    private UnitDecision AttackAimedTargetDecision(GameEntity target)
    {
      return new UnitDecision
      {
        UnitDecisionTypeId = UnitDecisionTypeId.AttackAimedTarget,
        TargetId = target.Id
      };
    }

    private void PrintDecision(UnitDecision decision, List<ScoreFactor> scores)
    {
      Debug.Log("----------------");

      Debug.Log($"Decision: {decision.UnitDecisionTypeId} == {scores.Select(x => x.Score).SumOrNull()}");

      foreach (ScoreFactor scoreFactor in scores)
        Debug.Log(scoreFactor.ToString());
    }
  }
}
