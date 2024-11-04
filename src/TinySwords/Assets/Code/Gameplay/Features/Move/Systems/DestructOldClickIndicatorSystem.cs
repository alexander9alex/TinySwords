using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class DestructOldClickIndicatorSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _destructOldMoveIndicatorRequests;
    private readonly IGroup<GameEntity> _moveClickIndicators;
    private readonly List<GameEntity> _buffer = new(1);

    public DestructOldClickIndicatorSystem(GameContext game)
    {
      _destructOldMoveIndicatorRequests = game.GetGroup(GameMatcher.DestructOldMoveIndicatorRequest);

      _moveClickIndicators = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveClickIndicator)
        .NoneOf(GameMatcher.CreatedNow));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _destructOldMoveIndicatorRequests.GetEntities(_buffer))
      foreach (GameEntity indicator in _moveClickIndicators)
      {
        indicator.isDestructed = true;
      }
    }
  }
}
