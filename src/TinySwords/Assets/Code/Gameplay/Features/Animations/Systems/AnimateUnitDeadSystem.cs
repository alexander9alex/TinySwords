using System.Collections.Generic;
using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.Animations.Systems
{
  public class AnimateUnitDeadSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly ITimeService _time;
    private readonly List<GameEntity> _buffer = new(16);

    public AnimateUnitDeadSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.DeathAnimator, GameMatcher.DisplayTimer, GameMatcher.HideTimer)
        .NoneOf(GameMatcher.SelfDestructTimer));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        if (entity.DisplayTimer > 0)
          entity.ReplaceDisplayTimer(entity.DisplayTimer - _time.DeltaTime);
        else
        {
          entity.DeathAnimator.HideSkull();
          entity.AddSelfDestructTimer(entity.HideTimer);
        }
      }
    }
  }
}
