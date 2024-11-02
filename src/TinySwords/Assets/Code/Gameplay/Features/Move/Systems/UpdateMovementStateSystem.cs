using System.Collections.Generic;
using System.Numerics;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class UpdateMovementStateSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public UpdateMovementStateSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveDirection));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        if (entity.MoveDirection.magnitude > float.Epsilon)
        {
          entity.isMove = true;
          entity.isIdle = false;
        }
        else
        {
          entity.isMove = false;
          entity.isIdle = true;
        }
      }
    }
  }
}
