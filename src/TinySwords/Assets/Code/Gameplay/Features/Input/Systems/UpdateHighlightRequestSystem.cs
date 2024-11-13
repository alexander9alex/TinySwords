using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
  public class UpdateHighlightRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selectionStarted;
    private readonly IGroup<GameEntity> _mousePositions;
    private readonly IGroup<GameEntity> _highlights;

    public UpdateHighlightRequestSystem(GameContext game)
    {
      _selectionStarted = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.SelectionStarted,
          GameMatcher.PositionOnScreen));

      _mousePositions = game.GetGroup(GameMatcher.MousePositionOnScreen);

      _highlights = game.GetGroup(GameMatcher.Highlight);
    }

    public void Execute()
    {
      foreach (GameEntity started in _selectionStarted)
      foreach (GameEntity mousePos in _mousePositions)
      foreach (GameEntity highlight in _highlights)
      {
        if (Vector2.Distance(started.PositionOnScreen, mousePos.MousePositionOnScreen) >= GameConstants.SelectionClickDelta)
        {
          highlight
            .ReplaceStartPosition(started.PositionOnScreen)
            .ReplaceEndPosition(mousePos.MousePositionOnScreen);
        }
      }
    }
  }
}
