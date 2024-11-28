using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.MoveInput.Systems
{
  public class CleanupEndDestinationRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _group;
    private readonly List<GameEntity> _buffer = new(32);

    public CleanupEndDestinationRequestSystem(GameContext game)
    {
      _group = game.GetGroup(GameMatcher.AllOf(GameMatcher.ChangeEndDestinationRequest));
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
