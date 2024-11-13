using System.Collections.Generic;
using Code.Gameplay.Features.Input.Factory;
using Entitas;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class CreateHighlightSystem : IExecuteSystem
  {
    private readonly IHighlightFactory _highlightFactory;

    private readonly IGroup<GameEntity> _createHighlightRequests;
    private readonly List<GameEntity> _buffer = new();

    public CreateHighlightSystem(GameContext game, IHighlightFactory highlightFactory)
    {
      _highlightFactory = highlightFactory;

      _createHighlightRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateHighlightRequest, GameMatcher.StartPosition));
    }

    public void Execute()
    {
      foreach (GameEntity request in _createHighlightRequests.GetEntities(_buffer))
      {
        _highlightFactory.CreateHighlight();

        request.isDestructed = true;
      }
    }
  }
}
