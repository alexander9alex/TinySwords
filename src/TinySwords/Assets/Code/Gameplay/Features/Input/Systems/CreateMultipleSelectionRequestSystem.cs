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
    private readonly IGroup<GameEntity> _selectionEnded;

    public CreateMultipleSelectionRequestSystem(GameContext game)
    {
      _selectionStarted = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.SelectionStarted,
          GameMatcher.PositionOnScreen));

      _selectionEnded = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.SelectionEnded,
          GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity started in _selectionStarted)
      foreach (GameEntity ended in _selectionEnded)
      {
        if (Vector2.Distance(started.PositionOnScreen, ended.PositionOnScreen) >= GameConstants.SelectionClickDelta)
        {
          CreateEntity.Empty()
            .With(x => x.isMultipleSelectionRequest = true);
        }
      }
    }
  }
}
