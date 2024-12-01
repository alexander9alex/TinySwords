using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.AttackIndicator.Systems
{
  public class DestructOldAttackIndicatorSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _destructOldAttackIndicatorRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _attackIndicators;
    private readonly List<GameEntity> _indicatorsBuffer = new(1);

    public DestructOldAttackIndicatorSystem(GameContext game)
    {
      _destructOldAttackIndicatorRequests = game.GetGroup(GameMatcher.DestructOldAttackIndicatorRequest);

      _attackIndicators = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AttackIndicator)
        .NoneOf(GameMatcher.CreatedNow));
    }

    public void Execute()
    {
      foreach (GameEntity request in _destructOldAttackIndicatorRequests.GetEntities(_requestsBuffer))
      {
        foreach (GameEntity indicator in _attackIndicators.GetEntities(_indicatorsBuffer))
          indicator.isDestructed = true;

        request.isDestructed = true;
      }
    }
  }
}
