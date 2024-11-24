using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.MoveIndicator.Systems
{
  public class DestructOldClickIndicatorSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _destructOldMoveIndicatorRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _moveClickIndicators;
    private readonly List<GameEntity> _indicatorsBuffer = new(1);

    public DestructOldClickIndicatorSystem(GameContext game)
    {
      _destructOldMoveIndicatorRequests = game.GetGroup(GameMatcher.DestructOldMoveIndicatorRequest);

      _moveClickIndicators = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveClickIndicator)
        .NoneOf(GameMatcher.CreatedNow));
    }

    public void Execute()
    {
      foreach (GameEntity request in _destructOldMoveIndicatorRequests.GetEntities(_requestsBuffer))
      {
        foreach (GameEntity indicator in _moveClickIndicators.GetEntities(_indicatorsBuffer))
        {
          indicator.isDestructed = true;
        }

        request.isDestructed = true;
      }
    }
  }
}
