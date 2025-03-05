using System.Collections.Generic;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Input.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interactions.Highlight.Systems
{
  public class CreateHighlightSystem : IExecuteSystem
  {
    private readonly IHighlightFactory _highlightFactory;

    private readonly IGroup<GameEntity> _interactions;
    private readonly IGroup<GameEntity> _inputs;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateHighlightSystem(GameContext game, IHighlightFactory highlightFactory)
    {
      _highlightFactory = highlightFactory;
      
      _interactions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Interaction,
          GameMatcher.StartPosition
        ).NoneOf(GameMatcher.Processed));

      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.MousePosition
        ));
    }

    public void Execute()
    {
      foreach (GameEntity interaction in _interactions.GetEntities(_buffer))
      foreach (GameEntity input in _inputs)
      {
        if (Vector2.Distance(interaction.StartPosition, input.MousePosition) >= GameConstants.SelectionClickDelta)
        {
          _highlightFactory.CreateHighlight(interaction.StartPosition, input.MousePosition);
          
          interaction.isProcessed = true;
        }
      }
    }
  }
}
