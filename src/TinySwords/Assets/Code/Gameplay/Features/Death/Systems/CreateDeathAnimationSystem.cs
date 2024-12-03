using Code.Gameplay.Features.Death.Factory;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Entitas;

namespace Code.Gameplay.Features.Death.Systems
{
  public class CreateDeathAnimationSystem : IExecuteSystem
  {
    private readonly IUnitDeathFactory _unitDeathFactory;
    private readonly ISoundService _soundService;
    
    private readonly IGroup<GameEntity> _entities;

    public CreateDeathAnimationSystem(GameContext game, IUnitDeathFactory unitDeathFactory, ISoundService soundService)
    {
      _unitDeathFactory = unitDeathFactory;
      _soundService = soundService;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.WorldPosition, GameMatcher.Dead));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        _unitDeathFactory.CreateDeathAnimation(entity.WorldPosition);
        _soundService.PlaySound(SoundId.Death, entity.WorldPosition);
      }
    }
  }
}
