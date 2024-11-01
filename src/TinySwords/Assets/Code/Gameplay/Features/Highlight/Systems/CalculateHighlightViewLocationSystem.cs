using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class CalculateHighlightViewLocationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _clickStarted;
    private readonly IGroup<GameEntity> _mousePositionInputs;

    public CalculateHighlightViewLocationSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.CenterPosition, GameMatcher.Size));

      _clickStarted = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.LeftClickStarted, GameMatcher.MousePosition));

      _mousePositionInputs = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MousePositionInput, GameMatcher.MousePosition));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity started in _clickStarted)
      foreach (GameEntity ended in _mousePositionInputs)
      {
        Vector2 pos = (started.MousePosition + ended.MousePosition) / 2;
        highlight.ReplaceCenterPosition(pos);
        
        Vector2 size = GetSize(started.MousePosition, ended.MousePosition);
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
