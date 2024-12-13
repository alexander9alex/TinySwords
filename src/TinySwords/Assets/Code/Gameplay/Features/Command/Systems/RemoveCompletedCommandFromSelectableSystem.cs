using System.Collections.Generic;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class RemoveCompletedCommandFromSelectableSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selectable;
    private readonly List<GameEntity> _buffer = new(32);
    private readonly ISelectableCommandService _selectableCommandService;

    public RemoveCompletedCommandFromSelectableSystem(GameContext game, ISelectableCommandService selectableCommandService)
    {
      _selectableCommandService = selectableCommandService;

      _selectable = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selectable, GameMatcher.UserCommand));
    }

    public void Execute()
    {
      foreach (GameEntity selectable in _selectable.GetEntities(_buffer))
      {
        if (_selectableCommandService.IsCommandCompleted(selectable))
          _selectableCommandService.RemoveCommand(selectable);
      }
    }
  }
}
