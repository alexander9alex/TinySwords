using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class CleanupMoveRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _group;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupMoveRequestSystem(GameContext game)
    {
      _group = game.GetGroup(GameMatcher.AllOf(GameMatcher.MoveRequest));
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
