using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class TickToMakeDecisionTimerSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;

    public TickToMakeDecisionTimerSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher.MakeDecisionTimer);
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.ReplaceMakeDecisionTimer(entity.MakeDecisionTimer - _time.DeltaTime);

        if (entity.MakeDecisionTimer <= 0)
        {
          entity.isMakeDecisionRequest = true;

          if (entity.hasMakeDecisionInterval)
            entity.ReplaceMakeDecisionTimer(entity.MakeDecisionInterval);
        }
      }
    }
  }
}
