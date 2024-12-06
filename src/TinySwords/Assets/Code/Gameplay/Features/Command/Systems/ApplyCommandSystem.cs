using System.Collections.Generic;
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
        if (_commandService.CanApplyCommand(command.CommandTypeId, request))
        {
          _commandService.ApplyCommand(command.CommandTypeId, request);
          command.isProcessed = true;
        }
        else
          _commandService.ProcessIncorrectCommand(command.CommandTypeId, request);
      }
    }
  }
}
