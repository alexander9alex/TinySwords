using System;
using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Units.Data;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class ProcessUnitDecisionSystem : IExecuteSystem
  {
    private readonly GameContext _game;

    private readonly IGroup<GameEntity> _units;
    private readonly List<GameEntity> _buffer = new(32);

    public ProcessUnitDecisionSystem(GameContext game)
    {
      _game = game;
      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.UnitDecision));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        ProcessUnitDecision(unit);
        unit.RemoveUnitDecision();
      }
    }

    private void ProcessUnitDecision(GameEntity unit)
    {
      UnitDecision decision = unit.UnitDecision;

      switch (decision.UnitDecisionTypeId)
      {
        case UnitDecisionTypeId.Stay:
          MakeStayDecision(unit);
          break;
        case UnitDecisionTypeId.MoveToEndDestination:
          MakeMoveToEndDestinationDecision(unit, decision);
          break;
        case UnitDecisionTypeId.MoveToTarget:
          MakeMoveToTargetDecision(unit, decision);
          break;
        case UnitDecisionTypeId.Attack:
          MakeAttackDecision(unit, decision);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static void MakeStayDecision(GameEntity unit)
    {
      if (unit.hasDestination)
        unit.ReplaceDestination(unit.WorldPosition);

      // if (unit.hasAimedTargetId)
        // unit.RemoveAimedTargetId();
    }

    private void MakeMoveToEndDestinationDecision(GameEntity unit, UnitDecision decision)
    {
      unit.ReplaceDestination(decision.Destination);

      // if (!unit.hasAimedTargetId)
        // return;

      // GameEntity aimedTarget = _game.GetEntityWithId(unit.AimedTargetId);

      // if (aimedTarget is not { hasWorldPosition: true })
        // return;

      // float distance = Vector2.Distance(aimedTarget.WorldPosition, decision.Destination);
      // if (distance > 0.25f)
        // unit.RemoveAimedTargetId();
    }

    private void MakeMoveToTargetDecision(GameEntity unit, UnitDecision decision)
    {
      unit.ReplaceDestination(decision.Destination);

      // if (unit.hasAimedTargetId && decision.TargetId != unit.AimedTargetId)
        // unit.RemoveAimedTargetId();
    }

    private void MakeAttackDecision(GameEntity unit, UnitDecision decision)
    {
      unit.ReplaceDestination(unit.WorldPosition);
      unit.ReplaceTargetId(decision.TargetId);

      // if (unit.hasAimedTargetId && decision.TargetId != unit.AimedTargetId)
        // unit.RemoveAimedTargetId();

      CreateEntity.Empty()
        .AddCasterId(unit.Id)
        .With(x => x.isAttackRequest = true);
    }
  }
}
