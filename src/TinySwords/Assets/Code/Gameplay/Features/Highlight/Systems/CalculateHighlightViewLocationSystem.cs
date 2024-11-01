using Entitas;

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
        .AllOf(GameMatcher.Highlight, GameMatcher.StartPosition, GameMatcher.EndPosition));

      _clickStarted = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.LeftClickStarted, GameMatcher.MousePosition));

      _mousePositionInputs = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MousePositionInput, GameMatcher.MousePosition));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity started in _clickStarted)
      foreach (GameEntity mousePosition in _mousePositionInputs)
      {
        highlight.ReplaceStartPosition(started.MousePosition);
        highlight.ReplaceEndPosition(mousePosition.MousePosition);
      }
    }
  }
}
