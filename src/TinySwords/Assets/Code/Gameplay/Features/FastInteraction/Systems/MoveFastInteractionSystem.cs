using System.Collections.Generic;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.FastInteraction.Systems
{
  public class MoveFastInteractionSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;

    private readonly IGroup<GameEntity> _fastInteractionRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public MoveFastInteractionSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _fastInteractionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FastInteraction, GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
      foreach (GameEntity request in _fastInteractionRequests.GetEntities(_buffer))
      {
        if (CanNotMakeMove(request))
          return;

        _commandService.CreateProcessCommandRequest(CommandTypeId.Move, request);

        request.isProcessed = true;
      }
    }

    private bool AllSelectedCanMove()
    {
      foreach (GameEntity selected in _selected)
      {
        if (!selected.isMovable)
          return false;
      }

      return true;
    }

    private bool CanNotMakeMove(GameEntity request) =>
      !_commandService.CanApplyCommand(CommandTypeId.Move, request) || !SelectedCanMove();

    private bool SelectedCanMove() =>
      HasSelected() && AllSelectedCanMove();

    private bool HasSelected() =>
      _selected.count > 0;
  }
}
