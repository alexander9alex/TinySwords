using System.Collections.Generic;
using Code.Gameplay.Features.Command.Services;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ApplyCommandSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;
    private readonly ISoundService _soundService;

    private readonly IGroup<GameEntity> _applyCommandRequests;
    private readonly IGroup<GameEntity> _selectedCommands;
    private readonly List<GameEntity> _buffer = new(1);

    public ApplyCommandSystem(GameContext game, ICommandService commandService, ISoundService soundService)
    {
      _commandService = commandService;
      _soundService = soundService;

      _applyCommandRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.ApplyCommand, GameMatcher.PositionOnScreen));
      _selectedCommands = game.GetGroup(GameMatcher.AllOf(GameMatcher.Command, GameMatcher.SelectedCommand, GameMatcher.CommandTypeId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _applyCommandRequests)
      foreach (GameEntity command in _selectedCommands.GetEntities(_buffer))
      {
        if (_commandService.CanApplyCommand(command, request))
          _commandService.ApplyCommand(command, request);
        else
          _soundService.PlaySound(SoundId.IncorrectCommand);
      }
    }
  }
}
