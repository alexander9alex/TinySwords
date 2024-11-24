using System;
using System.Collections.Generic;
using Code.Gameplay.Features.Units.Data;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.AI.Systems
{
  public class ProcessUnitDecisionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _units;
    private readonly List<GameEntity> _buffer = new(32);

    public ProcessUnitDecisionSystem(GameContext game)
    {
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

    private static void MakeStayDecision(GameEntity unit) =>
      unit.ReplaceDestination(unit.WorldPosition);

    private void MakeMoveToEndDestinationDecision(GameEntity unit, UnitDecision decision) =>
      unit.ReplaceDestination(decision.Position);

    private void MakeMoveToTargetDecision(GameEntity unit, UnitDecision decision) =>
      unit.ReplaceDestination(decision.Position);

    private void MakeAttackDecision(GameEntity unit, UnitDecision decision)
    {
      unit.ReplaceDestination(unit.WorldPosition);
      
      Debug.Log("Attack Request");
    }
  }
}
