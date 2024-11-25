using System.Collections.Generic;
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
          entity.isAlive = false;
          entity.isSelectable = false;
          entity.isSelected = false;
          entity.isDead = true;

          entity.isDestructed = true;
        }
      }
    }
  }
}
