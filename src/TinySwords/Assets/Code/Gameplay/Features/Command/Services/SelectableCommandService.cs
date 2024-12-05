using System;
using Code.Gameplay.Features.Command.Data;

namespace Code.Gameplay.Features.Command.Services
{
  class SelectableCommandService : ISelectableCommandService
  {
    private readonly GameContext _gameContext;

    public SelectableCommandService(GameContext gameContext) =>
      _gameContext = gameContext;

    public bool CommandCompleted(GameEntity selectable)
    {
      return selectable.CommandTypeId switch
      {
        CommandTypeId.Move => MoveCommandCompleted(selectable),
        CommandTypeId.MoveWithAttack => MoveWithAttackCommandCompleted(selectable),
        CommandTypeId.AimedAttack => AimedAttackCommandCompleted(selectable),
        _ => throw new ArgumentOutOfRangeException()
      };
    }

    public void RemoveCommand(GameEntity selectable)
    {
      switch (selectable.CommandTypeId)
      {
        case CommandTypeId.Move: 
          RemoveMoveCommand(selectable);
          break;
        case CommandTypeId.MoveWithAttack:
          RemoveMoveWithAttackCommand(selectable);
          break;
        case CommandTypeId.AimedAttack:
          RemoveAimedAttackCommand(selectable);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void RemoveMoveCommand(GameEntity entity)
    {
      if (entity.hasEndDestination)
        entity.RemoveEndDestination();

      entity.RemoveCommandTypeId();
    }

    private bool MoveCommandCompleted(GameEntity entity) =>
      !entity.hasEndDestination;

    private bool MoveWithAttackCommandCompleted(GameEntity entity) =>
      MoveCommandCompleted(entity);

    private bool AimedAttackCommandCompleted(GameEntity entity)
    {
      if (!entity.hasAimedTargetId)
        return true;

      GameEntity target = _gameContext.GetEntityWithId(entity.AimedTargetId);

      return target == null || target.isDead;
    }

    private void RemoveMoveWithAttackCommand(GameEntity entity) =>
      RemoveMoveCommand(entity);

    private void RemoveAimedAttackCommand(GameEntity entity)
    {
      if (entity.hasAimedTargetId)
        entity.RemoveAimedTargetId();

      entity.RemoveCommandTypeId();

      if (entity.hasEndDestination)
        entity.RemoveEndDestination();
    }
  }
}
