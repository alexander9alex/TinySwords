using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class UpdateTransformAfterSpawningSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(64);

    public UpdateTransformAfterSpawningSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Transform, GameMatcher.WorldPosition, GameMatcher.UpdatePositionAfterSpawning));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.Transform.position = entity.WorldPosition;
        entity.isPositionUpdated = true;
        
        entity.isUpdatePositionAfterSpawning = false;
      }
    }
  }
}
