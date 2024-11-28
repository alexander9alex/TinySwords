using System;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Data;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ApplyCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _applyCommandRequests;
    private readonly IGroup<GameEntity> _selectedCommands;

    public ApplyCommandSystem(GameContext game)
    {
      _applyCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ApplyCommand, GameMatcher.PositionOnScreen));
      
      _selectedCommands = game.GetGroup(GameMatcher.AllOf(GameMatcher.Command, GameMatcher.SelectedCommand, GameMatcher.CommandTypeId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _applyCommandRequests)
      foreach (GameEntity command in _selectedCommands)
      {
        ApplyCommand(command, request);

        CreateEntity.Empty()
          .With(x => x.isCancelCommand = true);
      }
    }

    private static void ApplyCommand(GameEntity command, GameEntity request)
    {
      GameEntity entity = CreateEntity.Empty()
        .AddCommandTypeId(command.CommandTypeId)
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isProcessCommand = true);

      switch (command.CommandTypeId)
      {
        case CommandTypeId.Move:
          entity.isMoveCommand = true;
          break;
        case CommandTypeId.MoveWithAttack:
          entity.isMoveWithAttackCommand = true;
          break;
        case CommandTypeId.AimedAttack:
          entity.isAimedAttackCommand = true;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
