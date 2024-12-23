using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class CalculateAnimationSpeedSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;

    public CalculateAnimationSpeedSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AnimationSpeedChanger, GameMatcher.Speed));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.AnimationSpeedChanger.ChangeSpeed(entity.Speed * _time.TimeScale);
      }
    }
  }
}
