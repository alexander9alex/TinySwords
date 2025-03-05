using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Interactions.Highlight.Systems
{
  public class DestroyHighlightSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _interactions;
    private readonly List<GameEntity> _buffer = new(1);

    public DestroyHighlightSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher.Highlight);

      _interactions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Interaction,
          GameMatcher.CompleteInteraction
        ));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _interactions)
      foreach (GameEntity highlight in _highlights.GetEntities(_buffer))
      {
        highlight.isDestructed = true;
      }
    }
  }
}
