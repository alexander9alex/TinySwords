using System.Collections.Generic;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateSingleSelectionRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _actionEnded;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateSingleSelectionRequestSystem(GameContext game)
    {
      _actionEnded = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.ActionEnded,
          GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));
    }

    public void Execute()
    {
      foreach (GameEntity ended in _actionEnded.GetEntities(_buffer))
      {
        ended
          .With(x => x.isSingleSelectionRequest = true);
      }
    }
  }
}
