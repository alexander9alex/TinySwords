using Entitas;

namespace Code.Gameplay.Features.Effects.Systems
{
  public class ProcessDamageEffectSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly IGroup<GameEntity> _damageEffects;

    public ProcessDamageEffectSystem(GameContext game)
    {
      _game = game;
      _damageEffects = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.DamageEffect, GameMatcher.TargetId, GameMatcher.EffectValue));
    }

    public void Execute()
    {
      foreach (GameEntity effect in _damageEffects)
      {
        GameEntity target = _game.GetEntityWithId(effect.TargetId);

        if (target.isAlive && target.hasCurrentHp)
        {
          target.ReplaceCurrentHp(target.CurrentHp - effect.EffectValue);
          target.isAnimateTakenDamage = true;
        }
        effect.isDestructed = true;
      }
    }
  }
}
