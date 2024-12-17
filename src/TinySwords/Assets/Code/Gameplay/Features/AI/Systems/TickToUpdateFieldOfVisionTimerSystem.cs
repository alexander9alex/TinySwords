using System.Collections.Generic;
using Code.Gameplay.Constants;
using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class TickToUpdateFieldOfVisionTimerSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public TickToUpdateFieldOfVisionTimerSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher.AllOf(
        GameMatcher.UpdateFieldOfVisionTimer,
        GameMatcher.TimeSinceLastVisionUpdated
      ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.ReplaceUpdateFieldOfVisionTimer(entity.UpdateFieldOfVisionTimer - _time.DeltaTime);
        entity.ReplaceUpdateFieldOfVisionTimer(entity.TimeSinceLastVisionUpdated + _time.DeltaTime);

        if (entity.UpdateFieldOfVisionTimer > 0 && entity.TimeSinceLastVisionUpdated > GameConstants.MinLastTimeToUpdateFieldOfVision)
          continue;

        entity.isUpdateFieldOfVision = true;

        if (entity.hasUpdateFieldOfVisionInterval)
          entity.ReplaceUpdateFieldOfVisionTimer(entity.UpdateFieldOfVisionInterval);
        else
          entity.RemoveUpdateFieldOfVisionTimer();

        entity.ReplaceTimeSinceLastVisionUpdated(0);
      }
    }
  }
}
