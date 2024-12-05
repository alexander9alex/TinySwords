using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Indicators.Factory;
using Entitas;

namespace Code.Gameplay.Features.Indicators.Systems
{
  public class CreateIndicatorSystem : IExecuteSystem
  {
    private readonly IIndicatorFactory _indicatorFactory;

    private readonly IGroup<GameEntity> _createIndicatorRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateIndicatorSystem(GameContext game, IIndicatorFactory indicatorFactory)
    {
      _indicatorFactory = indicatorFactory;

      _createIndicatorRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateIndicator, GameMatcher.IndicatorTypeId, GameMatcher.WorldPosition));
    }

    public void Execute()
    {
      foreach (GameEntity request in _createIndicatorRequests.GetEntities(_buffer))
      {
        _indicatorFactory.CreateIndicator(request);

        CreateEntity.Empty()
          .With(x => x.isDestructOldIndicator = true);

        request.isDestructed = true;
      }
    }
  }
}
