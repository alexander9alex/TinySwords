using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class MakeDecisionNowSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public MakeDecisionNowSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.MakeDecisionNowRequest, GameMatcher.MakeDecisionTimer, GameMatcher.TimeSinceLastDecision));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.isMakeDecisionRequest = true;
        entity.ReplaceTimeSinceLastDecision(0);

        if (entity.hasMakeDecisionInterval)
          entity.ReplaceMakeDecisionTimer(entity.MakeDecisionInterval);

        entity.isMakeDecisionNowRequest = false;
      }
    }
  }
}
