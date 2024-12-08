using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateMultipleSelectionRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _actionStarted;
    private readonly IGroup<GameEntity> _actionEnded;

    public CreateMultipleSelectionRequestSystem(GameContext game)
    {
      _actionStarted = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.ActionStarted,
          GameMatcher.ScreenPosition,
          GameMatcher.Processed));

      _actionEnded = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.ActionEnded,
          GameMatcher.ScreenPosition,
          GameMatcher.Processed));
    }

    public void Execute()
    {
      foreach (GameEntity ended in _actionEnded)
      foreach (GameEntity started in _actionStarted)
      {
        CreateEntity.Empty()
          .With(x => x.isMultipleSelectionRequest = true);
      }
    }
  }
}
