using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class ChangeHighlightViewLocationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _highlights;

    public ChangeHighlightViewLocationSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.RectTransform, GameMatcher.StartPosition, GameMatcher.EndPosition));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      {
        Vector2 pos = (highlight.StartPosition + highlight.EndPosition) / 2;
        Vector2 size = GetSize(highlight);
        
        highlight.RectTransform.position = pos;
        highlight.RectTransform.sizeDelta = size;
      }
    }

    private static Vector2 GetSize(GameEntity highlight)
    {
      Vector2 min = Vector2.Min(highlight.StartPosition, highlight.EndPosition);
      Vector2 max = Vector2.Max(highlight.StartPosition, highlight.EndPosition);
      
      return max - min;
    }
  }
}
