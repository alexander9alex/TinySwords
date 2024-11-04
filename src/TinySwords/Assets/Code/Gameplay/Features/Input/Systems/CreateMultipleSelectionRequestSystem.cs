using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateMultipleSelectionRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selectionStarted;
    private readonly IGroup<GameEntity> _mousePositions;

    public CreateMultipleSelectionRequestSystem(GameContext game)
    {
      _selectionStarted = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.SelectionStarted,
          GameMatcher.PositionOnScreen));

      _mousePositions = game.GetGroup(GameMatcher.MousePositionOnScreen);
    }

    public void Execute()
    {
      foreach (GameEntity started in _selectionStarted)
      foreach (GameEntity mousePos in _mousePositions)
      {
        if (Vector2.Distance(started.PositionOnScreen, mousePos.MousePositionOnScreen) >= GameConstants.SelectionClickDelta)
        {
          Debug.Log(Vector2.Distance(started.PositionOnScreen, mousePos.MousePositionOnScreen));
          
          CreateEntity.Empty()
            .With(x => x.isMultipleSelectionRequest = true)
            .AddStartPosition(started.PositionOnScreen)
            .AddEndPosition(mousePos.MousePositionOnScreen);
        }
      }
    }
  }
}
