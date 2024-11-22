using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class TickToMakeDecisionTimerSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _makeDecisionTimers;

    public TickToMakeDecisionTimerSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _makeDecisionTimers = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MakeDecisionTimer));
    }

    public void Execute()
    {
      foreach (GameEntity timer in _makeDecisionTimers)
      {
        timer.ReplaceMakeDecisionTimer(timer.MakeDecisionTimer - _time.DeltaTime);

        if (timer.MakeDecisionTimer <= 0)
        {
          timer.isMakeDecisionRequest = true;

          if (timer.hasMakeDecisionInterval)
            timer.ReplaceMakeDecisionTimer(timer.MakeDecisionInterval);
        }
      }
    }
  }
}
