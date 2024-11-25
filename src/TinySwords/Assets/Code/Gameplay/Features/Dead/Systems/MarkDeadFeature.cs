using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Dead.Systems
{
  public class MarkDeadFeature : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(16);

    public MarkDeadFeature(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CurrentHp, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        if (entity.CurrentHp <= 0)
        {
          if (entity.isUnit && entity.hasWorldPosition)
          {
            CreateEntity.Empty()
              .AddWorldPosition(entity.WorldPosition)
              .With(x => x.isAnimateDeath = true);
          }

          entity.isAlive = false;
          entity.isDead = true;
        }
      }
    }
  }
}
