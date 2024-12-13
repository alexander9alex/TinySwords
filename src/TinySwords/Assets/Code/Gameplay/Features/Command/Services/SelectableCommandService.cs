using System;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Command.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Services
{
  class SelectableCommandService : ISelectableCommandService
  {
    private readonly GameContext _gameContext;

    public SelectableCommandService(GameContext gameContext) =>
      _gameContext = gameContext;

    public bool IsCommandCompleted(GameEntity selectable)
    {
      return selectable.UserCommand.CommandTypeId switch
      {
        CommandTypeId.Move => MoveCommandCompleted(selectable),
        CommandTypeId.MoveWithAttack => MoveWithAttackCommandCompleted(selectable),
        CommandTypeId.AimedAttack => AimedAttackCommandCompleted(selectable),
        _ => throw new ArgumentOutOfRangeException()
      };
    }

    public void RemoveCommand(GameEntity selectable) =>
      selectable.RemoveUserCommand();

    private bool MoveCommandCompleted(GameEntity entity) =>
      Vector2.Distance(entity.UserCommand.WorldPosition.Value, entity.WorldPosition) <= GameConstants.StoppingDistance;

    private bool MoveWithAttackCommandCompleted(GameEntity entity) =>
      MoveCommandCompleted(entity);

    private bool AimedAttackCommandCompleted(GameEntity entity)
    {
      GameEntity target = _gameContext.GetEntityWithId(entity.UserCommand.TargetId.Value);
      return target == null || target.isDead;
    }
  }
}
