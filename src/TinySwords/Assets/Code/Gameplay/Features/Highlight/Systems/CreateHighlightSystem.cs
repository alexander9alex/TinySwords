using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Input.Factory;
using Entitas;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class CreateHighlightSystem : IExecuteSystem
  {
    private readonly IHighlightFactory _highlightFactory;

    private readonly IGroup<GameEntity> _multipleSelectionRequests;
    private readonly IGroup<GameEntity> _highlights;

    public CreateHighlightSystem(GameContext game, IHighlightFactory highlightFactory)
    {
      _highlightFactory = highlightFactory;

      _multipleSelectionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MultipleSelectionRequest, GameMatcher.StartPosition));

      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _multipleSelectionRequests)
      {
        if (_highlights.count == 0)
          CreateHighlight();
      }
    }

    private void CreateHighlight()
    {
      _highlightFactory.CreateHighlight();

      CreateEntity.Empty()
        .With(x => x.isUnselectPreviouslySelectedRequest = true);
    }
  }
}
