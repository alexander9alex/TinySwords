using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Units.Data;
using ModestTree;
using UnityEngine;

namespace Code.Gameplay.UtilityAI.Components
{
  public class GetInput
  {
    private const float False = 0;
    private const float True = 1;

    private readonly GameContext _game;

    public GetInput(GameContext game)
    {
      _game = game;
    }

    public float HasEndDestination(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasUserCommand)
        return False;

      return unit.UserCommand.WorldPosition.HasValue
        ? True
        : False;
    }

    public float PercentageDistanceToTarget(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasTargetBuffer || unit.TargetBuffer.IsEmpty() || !unit.hasWorldPosition || !unit.hasCollectTargetsRadius)
        return 0;

      GameEntity target = _game.GetEntityWithId(decision.TargetId.Value);

      float distance = Vector2.Distance(unit.WorldPosition, target.WorldPosition);
      return distance / (unit.CollectTargetsRadius + 1);
    }

    public float HasTargets(GameEntity unit, UnitDecision decision) =>
      unit.hasTargetBuffer && !unit.TargetBuffer.IsEmpty() ? True : False;

    public float CanReachToTarget(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasReachedTargetBuffer || unit.ReachedTargetBuffer.IsEmpty())
        return False;

      GameEntity target = _game.GetEntityWithId(decision.TargetId.Value);

      if (target is not { isAlive: true })
        return False;

      return unit.ReachedTargetBuffer.Contains(decision.TargetId.Value)
        ? True
        : False;
    }

    public float PercentageDistanceToReachedTarget(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasReachedTargetBuffer || unit.ReachedTargetBuffer.IsEmpty() || !unit.hasWorldPosition || !unit.hasAttackReach)
        return 1;
      
      GameEntity target = _game.GetEntityWithId(decision.TargetId.Value);

      return Vector2.Distance(unit.WorldPosition, target.WorldPosition) / (unit.AttackReach + 1);
    }

    public float UserCommandIsMove(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasUserCommand)
        return False;

      return unit.UserCommand.CommandTypeId == CommandTypeId.Move
        ? True
        : False;
    }

    public float UserCommandIsAimedAttack(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasUserCommand)
        return False;

      return unit.UserCommand.CommandTypeId == CommandTypeId.AimedAttack
        ? True
        : False;
    }

    public float MoveToAimedTarget(GameEntity unit, UnitDecision decision)
    {
      if (!unit.hasUserCommand)
        return False;

      if (unit.UserCommand.CommandTypeId != CommandTypeId.AimedAttack || !unit.UserCommand.TargetId.HasValue)
        return False;

      GameEntity aimedTarget = _game.GetEntityWithId(unit.UserCommand.TargetId.Value);

      if (aimedTarget is not { hasWorldPosition: true })
        return False;

      return Vector2.Distance(aimedTarget.WorldPosition, decision.Destination.Value) <= 0.25f
        ? True
        : False;
    }

    public float IsAimedTarget(GameEntity unit, UnitDecision decision)
    {
      return unit.UserCommand.TargetId == decision.TargetId
        ? True
        : False;
    }
  }
}
