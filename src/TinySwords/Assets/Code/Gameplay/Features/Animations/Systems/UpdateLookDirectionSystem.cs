using Entitas;

namespace Code.Gameplay.Features.Animations.Systems
{
  public class UpdateLookDirectionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;

    public UpdateLookDirectionSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveAnimator, GameMatcher.LookDirection));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.ReplaceLookDirection(entity.MoveAnimator.LookDirection);
      }
    }
  }
}
