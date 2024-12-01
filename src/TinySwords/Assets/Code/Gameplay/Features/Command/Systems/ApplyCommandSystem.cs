using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ApplyCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _applyCommandRequests;
    private readonly IGroup<GameEntity> _selectedCommands;
    private readonly ICommandService _commandService;

    public ApplyCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _applyCommandRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.ApplyCommand, GameMatcher.PositionOnScreen));
      _selectedCommands = game.GetGroup(GameMatcher.AllOf(GameMatcher.Command, GameMatcher.SelectedCommand, GameMatcher.CommandTypeId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _applyCommandRequests)
      foreach (GameEntity command in _selectedCommands)
      {
        if (_commandService.CanApplyCommand(command, request))
          _commandService.ApplyCommand(command, request);
      }
    }
  }
}
