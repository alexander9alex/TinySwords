using System.Collections.Generic;
using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.Destruct.Systems
{
  public class DestructAfterTimeSystem : IExecuteSystem
  {
    private readonly ITimeService _time;

    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public DestructAfterTimeSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.SelfDestructTimer));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {

        if (entity.SelfDestructTimer > 0)
          entity.ReplaceSelfDestructTimer(entity.SelfDestructTimer - _time.DeltaTime);
        else
          entity.isDestructed = true;
      }
    }
  }
}
