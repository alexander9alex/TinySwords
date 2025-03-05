using System.Collections.Generic;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Command.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.FastInteractions.Systems
{
  public class MoveFastInteractionSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;

    private readonly IGroup<GameEntity> _fastInteractions;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public MoveFastInteractionSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _fastInteractions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.FastInteraction,
          GameMatcher.ScreenPosition
        )
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
      foreach (GameEntity fastInteraction in _fastInteractions.GetEntities(_buffer))
      {
        if (CanNotMoveUnits(fastInteraction))
          continue;

        _commandService.ApplyCommand(CommandTypeId.Move, fastInteraction.ScreenPosition);

        fastInteraction.isProcessed = true;
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

    private bool CanNotMoveUnits(GameEntity fastInteraction) =>
      !CanMoveUnits(fastInteraction.ScreenPosition);

    private bool CanMoveUnits(Vector2 screenPos) =>
      _commandService.CanApplyCommand(CommandTypeId.Move, screenPos) && SelectedCanMove();

    private bool SelectedCanMove() =>
      HasSelected() && AllSelectedCanMove();

    private bool HasSelected() =>
      _selected.count > 0;
  }
}
