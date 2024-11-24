using System.Collections.Generic;
using Entitas;

namespace Code.Infrastructure.Views.Systems
{
  public class CleanupInitializeMarkerSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public CleanupInitializeMarkerSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.InitializationRequest, GameMatcher.Initialized));
    }

    public void Cleanup()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        entity.isInitializationRequest = false;
        entity.isInitialized = false;
      }
    }
  }
}
