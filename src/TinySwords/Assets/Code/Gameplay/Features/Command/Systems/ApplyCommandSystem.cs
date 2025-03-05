using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Command.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ApplyCommandSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;

    private readonly IGroup<GameEntity> _inputs;
    private readonly IGroup<GameEntity> _selectedCommands;
    private readonly List<GameEntity> _buffer = new(1);

    public ApplyCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.ApplyCommandInput,
          GameMatcher.MousePosition
        ));

      _selectedCommands = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Command,
          GameMatcher.SelectedCommand,
          GameMatcher.CommandTypeId
        ));
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs)
      foreach (GameEntity command in _selectedCommands.GetEntities(_buffer))
      {
        if (_commandService.CanApplyCommand(command.CommandTypeId, input.MousePosition))
          ApplyCommand(input, command, input.MousePosition);
        else
          ProcessIncorrectCommand(input.MousePosition, command.CommandTypeId);
      }
    }

    private void ApplyCommand(GameEntity input, GameEntity command, Vector2 screenPos)
    {
      _commandService.ApplyCommand(command.CommandTypeId, screenPos);

      command.isProcessed = true;
      input.With(x => x.isCancelCommandInput = true);
    }

    private void ProcessIncorrectCommand(Vector2 screenPos, CommandTypeId commandTypeId) =>
      _commandService.ProcessIncorrectCommand(commandTypeId, screenPos);
  }
}
