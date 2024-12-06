using System.Collections.Generic;
using Code.Gameplay.Features.FastInteraction.Services;
using Entitas;

namespace Code.Gameplay.Features.FastInteraction.Systems
{
  public class MoveFastInteractionSystem : IExecuteSystem
  {
    private readonly IFastInteractionService _fastInteractionService;

    private readonly IGroup<GameEntity> _fastInteractionRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public MoveFastInteractionSystem(GameContext game, IFastInteractionService fastInteractionService)
    {
      _fastInteractionService = fastInteractionService;

      _fastInteractionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FastInteraction, GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
      foreach (GameEntity request in _fastInteractionRequests.GetEntities(_buffer))
      {
        if (CantMoveSelected())
          return;

        _fastInteractionService.MoveSelected(request);
          
        request.isProcessed = true;
      }
    }

    private bool AllSelectedIsUnits()
    {
      foreach (GameEntity selected in _selected)
      {
        if (!selected.isUnit)
          return false;
      }

      return true;
    }

    private bool CantMoveSelected() =>
      !CanMoveSelected();

    private bool CanMoveSelected() =>
      HasSelected() && AllSelectedIsUnits();

    private bool HasSelected() =>
      _selected.count > 0;
  }
}
