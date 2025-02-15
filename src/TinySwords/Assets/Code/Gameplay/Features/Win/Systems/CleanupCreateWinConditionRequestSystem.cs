using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Win.Systems
{
  public class CleanupCreateWinConditionRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _group;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupCreateWinConditionRequestSystem(GameContext game)
    {
      _group = game.GetGroup(GameMatcher.CreateWinCondition);
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _group.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
