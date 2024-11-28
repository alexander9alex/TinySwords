using System;
using System.Collections.Generic;
using Code.Gameplay.Features.Command.Data;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class RemoveCompletedCommandSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly IGroup<GameEntity> _selectable;
    private readonly List<GameEntity> _buffer = new(32);

    public RemoveCompletedCommandSystem(GameContext game)
    {
      _game = game;

      _selectable = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selectable, GameMatcher.CommandTypeId));
    }

    public void Execute()
    {
      foreach (GameEntity selectable in _selectable.GetEntities(_buffer))
      {
        if (CommandCompleted(selectable))
          RemoveCommand(selectable);
      }
    }

    private bool CommandCompleted(GameEntity entity)
    {
      switch (entity.CommandTypeId)
      {
        case CommandTypeId.Move:
          return MoveCommandCompleted(entity);
        case CommandTypeId.MoveWithAttack:
          return MoveWithAttackCommandCompleted(entity);
        case CommandTypeId.AimedAttack:
          return AimedAttackCommandCompleted(entity);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void RemoveCommand(GameEntity entity)
    {
      switch (entity.CommandTypeId)
      {
        case CommandTypeId.Move:
          RemoveMoveCommand(entity);
          break;
        case CommandTypeId.MoveWithAttack:
          RemoveMoveWithAttackCommand(entity);
          break;
        case CommandTypeId.AimedAttack:
          RemoveAimedAttackCommand(entity);
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

    private bool AimedAttackCommandCompleted(GameEntity entity)
    {
      if (!entity.hasAimedTargetId)
        return true;

      GameEntity target = _game.GetEntityWithId(entity.AimedTargetId);

      return target == null || target.isDead;
    }

    private bool MoveWithAttackCommandCompleted(GameEntity entity) =>
      MoveCommandCompleted(entity);

    private bool MoveCommandCompleted(GameEntity entity) =>
      !entity.hasEndDestination;
  }
}
