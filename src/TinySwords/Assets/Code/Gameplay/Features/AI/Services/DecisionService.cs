using System;
using Code.Common.Extensions;
using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Features.AI.Services
{
  class DecisionService : IDecisionService
  {
    public void ProcessUnitDecision(GameEntity unit)
    {
      UnitDecision decision = unit.UnitDecision;

      switch (decision.UnitDecisionTypeId)
      {
        case UnitDecisionTypeId.Stay:
          MakeStayDecision(unit);
          break;
        case UnitDecisionTypeId.Move:
          MakeMoveDecision(unit, decision);
          break;
        case UnitDecisionTypeId.MoveToTarget:
        case UnitDecisionTypeId.MoveToAimedTarget:
          MakeMoveToTargetDecision(unit, decision);
          break;
        case UnitDecisionTypeId.MoveToAllyTarget:
          MakeMoveToAllyTargetDecision(unit, decision);
          break;
        case UnitDecisionTypeId.Attack:
        case UnitDecisionTypeId.AttackAimedTarget:
          MakeAttackDecision(unit, decision);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void RemoveUnitDecisions(GameEntity unit)
    {
      if (unit.hasDestination)
        unit.ReplaceDestination(unit.WorldPosition);

      if (unit.hasTargetId)
        unit.RemoveTargetId();

      if (unit.hasAllyTargetId)
        unit.RemoveAllyTargetId();

      unit.isFollowToTarget = false;
      unit.isAttackRequest = false;
    }

    private static void MakeStayDecision(GameEntity unit)
    {
      if (unit.hasDestination)
        unit.ReplaceDestination(unit.WorldPosition);
    }

    private void MakeMoveToAllyTargetDecision(GameEntity unit, UnitDecision decision)
    {
      MakeMoveToTargetDecision(unit, decision);
      unit.ReplaceAllyTargetId(decision.TargetId.Value);
    }

    private void MakeMoveDecision(GameEntity unit, UnitDecision decision) =>
      unit.ReplaceDestination(decision.Destination.Value);

    private void MakeMoveToTargetDecision(GameEntity unit, UnitDecision decision) =>
      unit.ReplaceTargetId(decision.TargetId.Value)
        .With(x => x.isFollowToTarget = true)
        .With(x => x.isNotifyAlliesAboutTarget = true);

    private void MakeAttackDecision(GameEntity unit, UnitDecision decision)
    {
      if (!unit.isCanAttackNow)
        return;

      unit.ReplaceDestination(unit.WorldPosition);
      unit.ReplaceTargetId(decision.TargetId.Value);
      unit.isAttackRequest = true;
    }
  }
}
