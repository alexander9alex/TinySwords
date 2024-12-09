using System.Collections.Generic;
using Code.Gameplay.Constants;
using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class TickToMakeDecisionTimerSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public TickToMakeDecisionTimerSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.MakeDecisionTimer, GameMatcher.TimeSinceLastDecision));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.ReplaceMakeDecisionTimer(entity.MakeDecisionTimer - _time.DeltaTime);
        entity.ReplaceTimeSinceLastDecision(entity.TimeSinceLastDecision + _time.DeltaTime);

        if (entity.MakeDecisionTimer <= 0 && entity.TimeSinceLastDecision > GameConstants.MinLastTimeToMakeNewDecision)
        {
          entity.isMakeDecisionRequest = true;

          if (entity.hasMakeDecisionInterval)
            entity.ReplaceMakeDecisionTimer(entity.MakeDecisionInterval);
          
          entity.ReplaceTimeSinceLastDecision(0);
        }
      }
    }
  }
}
