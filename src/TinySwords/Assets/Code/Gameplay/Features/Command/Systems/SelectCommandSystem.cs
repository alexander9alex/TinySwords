using System.Collections.Generic;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class SelectCommandSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;

    private readonly IGroup<GameEntity> _commands;
    private readonly List<GameEntity> _buffer = new(1);

    public SelectCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _commands = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Command, GameMatcher.CommandTypeId)
        .NoneOf(GameMatcher.SelectedCommand));
    }

    public void Execute()
    {
      foreach (GameEntity command in _commands.GetEntities(_buffer))
      {
        _commandService.SelectCommand(command.CommandTypeId);
        command.isSelectedCommand = true;
      }
    }
  }
}
