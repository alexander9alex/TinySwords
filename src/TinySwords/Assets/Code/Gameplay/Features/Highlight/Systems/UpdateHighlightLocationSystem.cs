using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class UpdateHighlightLocationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _highlights;

    public UpdateHighlightLocationSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.RectTransform, GameMatcher.CenterPosition, GameMatcher.Size));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      {
        highlight.RectTransform.position = highlight.CenterPosition;
        highlight.RectTransform.sizeDelta = highlight.Size;
      }
    }
  }
}
