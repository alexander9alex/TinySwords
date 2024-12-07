using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class TickToMakeDecisionTimerSystem : IExecuteSystem
  {
    private const float MinLastTimeToMakeNewDecision = 0.25f;
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;

    public TickToMakeDecisionTimerSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.MakeDecisionTimer, GameMatcher.TimeSinceLastDecision));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.ReplaceMakeDecisionTimer(entity.MakeDecisionTimer - _time.DeltaTime);
        entity.ReplaceTimeSinceLastDecision(entity.TimeSinceLastDecision + _time.DeltaTime);

        if (entity.MakeDecisionTimer <= 0 && entity.TimeSinceLastDecision > MinLastTimeToMakeNewDecision)
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
