using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interactions.Highlight.Systems
{
  public class UpdateHighlightLocationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _highlights;

    public UpdateHighlightLocationSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Highlight,
          GameMatcher.Transform,
          GameMatcher.StartPosition,
          GameMatcher.EndPosition
          ));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      {
        highlight.Transform.position = (highlight.EndPosition + highlight.StartPosition) / 2;
        highlight.Transform.localScale = highlight.EndPosition - highlight.StartPosition;
      }
    }
  }
}
