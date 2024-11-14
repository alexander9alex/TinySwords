using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateHighlightRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _actionStarted;
    private readonly IGroup<GameEntity> _mousePositions;
    private readonly IGroup<GameEntity> _highlights;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateHighlightRequestSystem(GameContext game)
    {
      _actionStarted = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.ActionStarted,
          GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));

      _mousePositions = game.GetGroup(GameMatcher.MousePositionOnScreen);

      _highlights = game.GetGroup(GameMatcher.Highlight);
    }

    public void Execute()
    {
      foreach (GameEntity started in _actionStarted.GetEntities(_buffer))
      foreach (GameEntity mousePos in _mousePositions)
      {
        if (_highlights.count > 0)
          return;

        if (Vector2.Distance(started.PositionOnScreen, mousePos.MousePositionOnScreen) >= GameConstants.SelectionClickDelta)
        {
          CreateEntity.Empty()
            .With(x => x.isCreateHighlightRequest = true)
            .AddStartPosition(started.PositionOnScreen)
            .AddEndPosition(mousePos.MousePositionOnScreen);

          started.isProcessed = true;
        }
      }
    }
  }
}
