using System.Collections.Generic;
using Code.Gameplay.Features.ProcessCommand.Services;
using Entitas;

namespace Code.Gameplay.Features.ProcessCommand.Systems
{
  public class ProcessMoveCommandSystem : IExecuteSystem
  {
    private readonly IProcessCommandService _processCommandService;

    private readonly IGroup<GameEntity> _processCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;

    public ProcessMoveCommandSystem(GameContext game, IProcessCommandService processCommandService)
    {
      _processCommandService = processCommandService;

      _processCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommandRequest, GameMatcher.MoveCommand, GameMatcher.CommandTypeId, GameMatcher.ScreenPosition));

      _selected = game.GetGroup(GameMatcher.AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processCommandRequests.GetEntities(_requestsBuffer))
      {
        _processCommandService.ProcessMoveCommand(request, _selected);
      }
    }
  }
}
