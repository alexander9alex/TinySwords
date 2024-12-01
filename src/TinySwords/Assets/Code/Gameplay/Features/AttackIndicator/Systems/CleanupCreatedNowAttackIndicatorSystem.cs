using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.AttackIndicator.Systems
{
  public class CleanupCreatedNowAttackIndicatorSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _createdNow;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupCreatedNowAttackIndicatorSystem(GameContext game)
    {
      _createdNow = game.GetGroup(GameMatcher.AllOf(GameMatcher.AttackIndicator, GameMatcher.CreatedNow));
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
