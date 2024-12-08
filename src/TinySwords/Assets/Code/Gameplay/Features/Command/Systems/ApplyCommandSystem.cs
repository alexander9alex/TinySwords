using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ApplyCommandSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;

    private readonly IGroup<GameEntity> _applyCommandRequests;
    private readonly IGroup<GameEntity> _selectedCommands;
    private readonly List<GameEntity> _buffer = new(1);

    public ApplyCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _applyCommandRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.ApplyCommand, GameMatcher.PositionOnScreen));
      _selectedCommands = game.GetGroup(GameMatcher.AllOf(GameMatcher.Command, GameMatcher.SelectedCommand, GameMatcher.CommandTypeId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _applyCommandRequests)
      foreach (GameEntity command in _selectedCommands.GetEntities(_buffer))
      {
        if (_commandService.CanApplyCommand(command.CommandTypeId, request.PositionOnScreen))
          ApplyCommand(command, request);
        else
          ProcessIncorrectCommand(command, request);
      }
    }

    private void ApplyCommand(GameEntity command, GameEntity request)
    {
      _commandService.ApplyCommand(command.CommandTypeId, request.PositionOnScreen);
      command.isProcessed = true;
          
      CreateEntity.Empty()
        .With(x => x.isCancelCommand = true);
    }

    private void ProcessIncorrectCommand(GameEntity command, GameEntity request) =>
      _commandService.ProcessIncorrectCommand(command.CommandTypeId, request);
  }
}
