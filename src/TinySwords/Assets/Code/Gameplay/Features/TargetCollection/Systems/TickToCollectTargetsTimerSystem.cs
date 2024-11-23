using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
  public class TickToCollectTargetsTimerSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;

    public TickToCollectTargetsTimerSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher.CollectTargetsTimer);
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.ReplaceCollectTargetsTimer(entity.CollectTargetsTimer - _time.DeltaTime);

        if (entity.CollectTargetsTimer <= 0)
        {
          entity.isCollectTargetsRequest = true;

          if (entity.hasCollectTargetsInterval)
            entity.ReplaceCollectTargetsTimer(entity.CollectTargetsInterval);
        }
      }
    }
  }
}
