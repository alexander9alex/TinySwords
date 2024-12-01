using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.MoveIndicator.Systems
{
  public class CleanupCreatedNowMoveIndicatorSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _createdNow;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupCreatedNowMoveIndicatorSystem(GameContext game)
    {
      _createdNow = game.GetGroup(GameMatcher.AllOf(GameMatcher.MoveIndicator, GameMatcher.CreatedNow));
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
