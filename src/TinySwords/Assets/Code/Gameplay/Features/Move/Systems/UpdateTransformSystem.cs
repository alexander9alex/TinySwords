using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class UpdateTransformSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;

    public UpdateTransformSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Transform, GameMatcher.WorldPosition));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        entity.Transform.position = entity.WorldPosition;
      }
    }
  }
}
