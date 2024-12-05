using System.Collections.Generic;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class CancelCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _cancelCommandRequests;
    private readonly List<GameEntity> _cancelCommandBuffer = new(1);

    private readonly IGroup<GameEntity> _selectedCommands;
    private readonly List<GameEntity> _selectedCommandsBuffer = new(1);
    private readonly ICommandService _commandService;

    public CancelCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _cancelCommandRequests = game.GetGroup(GameMatcher.CancelCommand);
      _selectedCommands = game.GetGroup(GameMatcher.AllOf(GameMatcher.Command, GameMatcher.SelectedCommand));
    }

    public void Execute()
    {
      foreach (GameEntity request in _cancelCommandRequests.GetEntities(_cancelCommandBuffer))
      foreach (GameEntity command in _selectedCommands.GetEntities(_selectedCommandsBuffer))
      {
        _commandService.CancelCommand(command);
        
        command.isDestructed = true;
        request.isDestructed = true;
      }
    }
  }
}
