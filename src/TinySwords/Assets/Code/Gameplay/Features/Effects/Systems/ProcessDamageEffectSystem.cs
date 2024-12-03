using Code.Gameplay.Features.Sounds.Services;
using Entitas;

namespace Code.Gameplay.Features.Effects.Systems
{
  public class ProcessDamageEffectSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly ISoundService _soundService;
    
    private readonly IGroup<GameEntity> _damageEffects;

    public ProcessDamageEffectSystem(GameContext game, ISoundService soundService)
    {
      _game = game;
      _soundService = soundService;
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

          _soundService.PlayTakingDamageSound(target);
        }
        
        effect.isDestructed = true;
      }
    }
  }
}
