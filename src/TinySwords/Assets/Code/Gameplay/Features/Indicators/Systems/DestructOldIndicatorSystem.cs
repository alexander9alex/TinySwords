using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Indicators.Systems
{
  public class DestructOldIndicatorSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _destructOldIndicatorRequests;
    private readonly List<GameEntity> _requestBuffer = new(1);

    private readonly IGroup<GameEntity> _indicators;
    private readonly List<GameEntity> _indicatorBuffer = new(1);

    public DestructOldIndicatorSystem(GameContext game)
    {
      _destructOldIndicatorRequests = game.GetGroup(GameMatcher.DestructOldIndicator);

      _indicators = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Indicator)
        .NoneOf(GameMatcher.CreatedNow));
    }

    public void Execute()
    {
      foreach (GameEntity request in _destructOldIndicatorRequests.GetEntities(_requestBuffer))
      {
        foreach (GameEntity indicator in _indicators.GetEntities(_indicatorBuffer))
          indicator.isDestructed = true;

        request.isDestructed = true;
      }
    }
  }
}
