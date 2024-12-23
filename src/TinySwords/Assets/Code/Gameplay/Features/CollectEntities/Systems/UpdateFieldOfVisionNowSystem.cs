using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.CollectEntities.Systems
{
  public class UpdateFieldOfVisionNowSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public UpdateFieldOfVisionNowSystem(GameContext game)
    {
      _entities = game.GetGroup(
        GameMatcher.AllOf(
          GameMatcher.UpdateFieldOfVisionNowRequest,
          GameMatcher.UpdateFieldOfVisionTimer,
          GameMatcher.TimeSinceLastVisionUpdated
        ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.isUpdateFieldOfVision = true;
        entity.ReplaceTimeSinceLastVisionUpdated(0);

        if (entity.hasUpdateFieldOfVisionInterval)
          entity.ReplaceUpdateFieldOfVisionTimer(entity.UpdateFieldOfVisionInterval);
        
        entity.isUpdateFieldOfVisionNowRequest = false;
      }
    }
  }
}
