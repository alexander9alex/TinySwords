using System.Collections.Generic;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.ProcessCommand.Systems
{
  public class ProcessMoveCommandSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;

    private readonly IGroup<GameEntity> _processCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;

    public ProcessMoveCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _processCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommandRequest, GameMatcher.MoveCommand, GameMatcher.CommandTypeId, GameMatcher.ScreenPosition));

      _selected = game.GetGroup(GameMatcher.AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processCommandRequests.GetEntities(_requestsBuffer))
      {
        _commandService.ProcessMoveCommand(request, _selected);
      }
    }
  }
}
