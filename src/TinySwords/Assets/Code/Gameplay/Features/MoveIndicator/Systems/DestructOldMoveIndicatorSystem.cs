using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.MoveIndicator.Systems
{
  public class DestructOldMoveIndicatorSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _destructOldMoveIndicatorRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _moveIndicators;
    private readonly List<GameEntity> _indicatorsBuffer = new(1);

    public DestructOldMoveIndicatorSystem(GameContext game)
    {
      _destructOldMoveIndicatorRequests = game.GetGroup(GameMatcher.DestructOldMoveIndicatorRequest);

      _moveIndicators = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveIndicator)
        .NoneOf(GameMatcher.CreatedNow));
    }

    public void Execute()
    {
      foreach (GameEntity request in _destructOldMoveIndicatorRequests.GetEntities(_requestsBuffer))
      {
        foreach (GameEntity indicator in _moveIndicators.GetEntities(_indicatorsBuffer))
          indicator.isDestructed = true;

        request.isDestructed = true;
      }
    }
  }
}
