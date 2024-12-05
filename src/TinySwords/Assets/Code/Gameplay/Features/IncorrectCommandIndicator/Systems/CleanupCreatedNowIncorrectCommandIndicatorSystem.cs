using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.IncorrectCommandIndicator.Systems
{
  public class CleanupCreatedNowIncorrectCommandIndicatorSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _createdNow;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupCreatedNowIncorrectCommandIndicatorSystem(GameContext game)
    {
      _createdNow = game.GetGroup(GameMatcher.AllOf(GameMatcher.IncorrectCommandIndicator, GameMatcher.CreatedNow));
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
