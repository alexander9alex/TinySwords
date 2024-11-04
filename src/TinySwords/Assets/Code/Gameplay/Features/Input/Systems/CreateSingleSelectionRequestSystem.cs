using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateSingleSelectionRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selectionStarted;
    private readonly IGroup<GameEntity> _selectionEnded;

    public CreateSingleSelectionRequestSystem(GameContext game)
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
      foreach (GameEntity ended in _selectionEnded)
      foreach (GameEntity started in _selectionStarted)
      {
        if (Vector2.Distance(started.PositionOnScreen, ended.PositionOnScreen) < GameConstants.SelectionClickDelta)
        {
          CreateEntity.Empty()
            .With(x => x.isSingleSelectionRequest = true)
            .AddPositionOnScreen(ended.PositionOnScreen);
        }
      }
    }
  }
}
