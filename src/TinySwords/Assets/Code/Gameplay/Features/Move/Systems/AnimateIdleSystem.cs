using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class AnimateIdleSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;

    public AnimateIdleSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Idle, GameMatcher.MoveAnimator));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.MoveAnimator.AnimateIdle();
      }
    }
  }
}
