using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Indicators.Systems
{
  public class CleanupCreatedNowIndicatorSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _indicators;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupCreatedNowIndicatorSystem(GameContext game)
    {
      _indicators = game.GetGroup(GameMatcher.AllOf(GameMatcher.Indicator, GameMatcher.CreatedNow));
    }

    public void Cleanup()
    {
      foreach (GameEntity indicator in _indicators.GetEntities(_buffer))
      {
        indicator.isCreatedNow = false;
      }
    }
  }
}
