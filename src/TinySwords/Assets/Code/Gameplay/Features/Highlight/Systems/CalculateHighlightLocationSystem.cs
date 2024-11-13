using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class CalculateHighlightLocationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _highlights;

    public CalculateHighlightLocationSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.CenterPosition, GameMatcher.Size, GameMatcher.StartPosition, GameMatcher.EndPosition));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      {
        Vector2 pos = (highlight.StartPosition + highlight.EndPosition) / 2;
        highlight.ReplaceCenterPosition(pos);

        Vector2 size = GetSize(highlight.StartPosition, highlight.EndPosition);
        highlight.ReplaceSize(size);
      }
    }

    private static Vector2 GetSize(Vector2 start, Vector2 end)
    {
      Vector2 min = Vector2.Min(start, end);
      Vector2 max = Vector2.Max(start, end);

      return max - min;
    }
  }
}
