using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupSingleSelectionRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _singleSelectionRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupSingleSelectionRequestSystem(GameContext game)
    {
      _singleSelectionRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.SingleSelectionRequest));
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _singleSelectionRequests.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
