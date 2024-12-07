using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
  public class TickToCollectTargetTimerSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;

    public TickToCollectTargetTimerSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher.CollectTargetTimer);
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.ReplaceCollectTargetTimer(entity.CollectTargetTimer - _time.DeltaTime);

        if (entity.CollectTargetTimer <= 0)
        {
          entity.isCollectTargets = true;

          if (entity.hasCollectTargetInterval)
            entity.ReplaceCollectTargetTimer(entity.CollectTargetInterval);
          else
            entity.RemoveCollectTargetTimer();
        }
      }
    }
  }
}
