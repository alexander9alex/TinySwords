using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class AnimateWalkSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;

    public AnimateWalkSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Move, GameMatcher.MoveAnimator, GameMatcher.MoveDirection));
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
