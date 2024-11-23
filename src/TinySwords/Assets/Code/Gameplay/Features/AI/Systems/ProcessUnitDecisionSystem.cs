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
    private List<GameEntity> _buffer = new(32);

    public ProcessUnitDecisionSystem(GameContext game)
    {
      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.UnitDecision));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        Debug.Log(unit.UnitDecision.UnitDecisionTypeId);

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
        case UnitDecisionTypeId.Move:
          MakeMoveDecision(unit, decision);
          break;
        case UnitDecisionTypeId.Attack:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static void MakeStayDecision(GameEntity unit)
    {
      if (unit.hasDestination)
        unit.RemoveDestination();
    }

    private void MakeMoveDecision(GameEntity unit, UnitDecision decision) =>
      unit.ReplaceDestination(decision.Destination);
  }
}
