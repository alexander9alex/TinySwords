using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Highlight.Systems
{
  public class CleanupHighlightSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _clickEnded;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupHighlightSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher.AllOf(GameMatcher.Highlight));
      _clickEnded = game.GetGroup(GameMatcher.AllOf(GameMatcher.LeftClickEnded));
    }

    public void Cleanup()
    {
      foreach (GameEntity _ in _clickEnded)
      foreach (GameEntity highlight in _highlights.GetEntities(_buffer))
      {
        highlight.isDestructed = true;
      }
    }
  }
}
