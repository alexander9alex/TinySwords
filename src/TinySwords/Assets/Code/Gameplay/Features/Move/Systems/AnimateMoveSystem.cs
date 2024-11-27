using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class AnimateMoveSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;

    public AnimateMoveSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Move, GameMatcher.MoveAnimator, GameMatcher.MoveDirection, GameMatcher.NotAttacking, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.MoveAnimator.AnimateWalk(entity.MoveDirection);
      }
    }
  }
}

