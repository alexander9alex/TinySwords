using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class CleanupCreatedNowSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _createdNow;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupCreatedNowSystem(GameContext game)
    {
      _createdNow = game.GetGroup(GameMatcher.AllOf(GameMatcher.CreatedNow));
    }

    public void Cleanup()
    {
      foreach (GameEntity createdNow in _createdNow.GetEntities(_buffer))
      {
        createdNow.isCreatedNow = false;
      }
    }
  }
}
