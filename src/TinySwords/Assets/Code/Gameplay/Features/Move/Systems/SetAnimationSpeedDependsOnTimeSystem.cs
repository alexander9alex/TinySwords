using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class SetAnimationSpeedDependsOnTimeSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _entities;

    public SetAnimationSpeedDependsOnTimeSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AnimationSpeedChanger, GameMatcher.Speed));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.AnimationSpeedChanger.ChangeSpeed(_time.TimeScale);
      }
    }
  }
}
