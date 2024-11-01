using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class CleanupUnselectRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _unselectRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupUnselectRequestSystem(GameContext game)
    {
      _unselectRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.UnselectPreviouslySelectedRequest));
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _unselectRequests.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
