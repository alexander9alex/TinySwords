using System.Collections.Generic;
using Entitas;
using ModestTree;

namespace Code.Gameplay.Features.MoveInput.Systems
{
  public class UpdateRunAwayStateSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(64);

    public UpdateRunAwayStateSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.UpdateRunAwayState, GameMatcher.TargetBuffer));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.isRunAway = EntityHasTargets(entity);

        entity.isUpdateRunAwayState = false;
      }
    }

    private static bool EntityHasTargets(GameEntity entity) =>
      !entity.TargetBuffer.IsEmpty();
  }
}
