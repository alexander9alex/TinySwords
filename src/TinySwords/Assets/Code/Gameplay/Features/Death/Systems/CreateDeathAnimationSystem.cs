using Code.Gameplay.Features.Death.Factory;
using Entitas;

namespace Code.Gameplay.Features.Death.Systems
{
  public class CreateDeathAnimationSystem : IExecuteSystem
  {
    private readonly IUnitDeathFactory _unitDeathFactory;
    private readonly IGroup<GameEntity> _entities;

    public CreateDeathAnimationSystem(GameContext game, IUnitDeathFactory unitDeathFactory)
    {
      _unitDeathFactory = unitDeathFactory;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.WorldPosition, GameMatcher.Dead));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        _unitDeathFactory.CreateDeathAnimation(entity.WorldPosition);
      }
    }
  }
}
