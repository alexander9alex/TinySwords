using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class CalculateHighlightLocationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _multipleSelectionRequests;
    
    public CalculateHighlightLocationSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.CenterPosition, GameMatcher.Size));

      _multipleSelectionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MultipleSelectionRequest, GameMatcher.StartPosition, GameMatcher.EndPosition));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity request in _multipleSelectionRequests)
      {
        Vector2 pos = (request.StartPosition + request.EndPosition) / 2;
        highlight.ReplaceCenterPosition(pos);
        
        Vector2 size = GetSize(request.StartPosition, request.EndPosition);
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
