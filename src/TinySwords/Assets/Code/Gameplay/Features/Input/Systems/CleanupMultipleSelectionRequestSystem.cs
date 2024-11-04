using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupMultipleSelectionRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _multipleSelectionRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupMultipleSelectionRequestSystem(GameContext game)
    {
      _multipleSelectionRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.MultipleSelectionRequest));
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _multipleSelectionRequests.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
