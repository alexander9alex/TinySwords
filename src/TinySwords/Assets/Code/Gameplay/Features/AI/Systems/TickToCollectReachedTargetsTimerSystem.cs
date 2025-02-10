using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class TickToCollectReachedTargetsTimerSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;

    public TickToCollectReachedTargetsTimerSystem(GameContext game, ITimeService time)
    {
      _time = time;
      // _entities = game.GetGroup(GameMatcher.CollectReachedTargetsTimer);
    }

    public void Execute()
    {
      // foreach (GameEntity entity in _entities)
      // {
      //   entity.ReplaceCollectReachedTargetsTimer(entity.CollectReachedTargetsTimer - _time.DeltaTime);
      //
      //   if (entity.CollectReachedTargetsTimer <= 0)
      //   {
      //     entity.isCollectReachedTargetsRequest = true;
      //
      //     if (entity.hasCollectReachedTargetsInterval)
      //       entity.ReplaceCollectReachedTargetsTimer(entity.CollectReachedTargetsInterval);
      //   }
      // }
    }
  }
}
