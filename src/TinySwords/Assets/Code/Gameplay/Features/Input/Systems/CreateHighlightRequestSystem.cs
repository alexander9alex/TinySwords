using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateHighlightRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selectionStarted;
    private readonly IGroup<GameEntity> _mousePositions;
    private readonly IGroup<GameEntity> _highlights;
    
    public CreateHighlightRequestSystem(GameContext game)
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
      {
        if (_highlights.count > 0)
          return;
        
        if (Vector2.Distance(started.PositionOnScreen, mousePos.MousePositionOnScreen) >= GameConstants.SelectionClickDelta)
        {
          CreateEntity.Empty()
            .With(x => x.isCreateHighlightRequest = true)
            .AddStartPosition(started.PositionOnScreen)
            .AddEndPosition(mousePos.MousePositionOnScreen);
        }
      }
    }
  }
}
