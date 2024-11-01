using Code.Gameplay.Features.Input.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class CreateHighlightSystem : IExecuteSystem
  {
    private readonly IHighlightFactory _highlightFactory;
    
    private readonly IGroup<GameEntity> _clickStarted;
    private readonly IGroup<GameEntity> _mousePositionInputs;
    private readonly IGroup<GameEntity> _highlights;

    public CreateHighlightSystem(GameContext game, IHighlightFactory highlightFactory)
    {
      _highlightFactory = highlightFactory;
      _clickStarted = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.LeftClickStarted, GameMatcher.MousePosition));
      
      _mousePositionInputs = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MousePositionInput, GameMatcher.MousePosition));

      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight));
    }

    public void Execute()
    {
      foreach (GameEntity started in _clickStarted)
      foreach (GameEntity mousePos in _mousePositionInputs)
      {
        if (_highlights.count > 0)
          return;
        
        if (Vector2.Distance(started.MousePosition, mousePos.MousePosition) >= float.Epsilon)
          _highlightFactory.CreateHighlight(started.MousePosition, mousePos.MousePosition);
      }
    }
  }
}
