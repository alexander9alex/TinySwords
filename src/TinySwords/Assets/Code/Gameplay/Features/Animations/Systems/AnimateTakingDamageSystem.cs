using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Animations.Systems
{
  public class AnimateTakingDamageSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public AnimateTakingDamageSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AnimateTakenDamage, GameMatcher.DamageTakenAnimator));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.DamageTakenAnimator.AnimateDamage();

        entity.isAnimateTakenDamage = false;
      }
    }
  }
}
