using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class UpdateWorldPositionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entity;

    public UpdateWorldPositionSystem(GameContext game)
    {
      _entity = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.WorldPosition, GameMatcher.Transform)
        .NoneOf(GameMatcher.UpdatePositionAfterSpawning));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entity)
      {
        entity.ReplaceWorldPosition(entity.Transform.position);
      }
    }
  }
}
