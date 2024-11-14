using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class UpdateHighlightLocationRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _actionStarted;
    private readonly IGroup<GameEntity> _mousePositions;
    private readonly IGroup<GameEntity> _highlights;

    public UpdateHighlightLocationRequestSystem(GameContext game)
    {
      _actionStarted = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.ActionStarted,
          GameMatcher.PositionOnScreen,
          GameMatcher.Processed));

      _mousePositions = game.GetGroup(GameMatcher.MousePositionOnScreen);

      _highlights = game.GetGroup(GameMatcher.Highlight);
    }

    public void Execute()
    {
      foreach (GameEntity started in _actionStarted)
      foreach (GameEntity mousePos in _mousePositions)
      foreach (GameEntity highlight in _highlights)
      {
        highlight
          .ReplaceStartPosition(started.PositionOnScreen)
          .ReplaceEndPosition(mousePos.MousePositionOnScreen);
      }
    }
  }
}
