using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class DestroyHighlightSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _actionEnded;
    private readonly List<GameEntity> _buffer = new(1);

    public DestroyHighlightSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher.AllOf(GameMatcher.Highlight));
      _actionEnded = game.GetGroup(GameMatcher.AllOf(GameMatcher.InteractionEnded));
    }

    public void Cleanup()
    {
      foreach (GameEntity _ in _actionEnded)
      foreach (GameEntity highlight in _highlights.GetEntities(_buffer))
      {
        highlight.isDestructed = true;
      }
    }
  }
}
