using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupMousePositionSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _mousePositions;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupMousePositionSystem(GameContext game)
    {
      _mousePositions = game.GetGroup(GameMatcher.AllOf(GameMatcher.MousePosition));
    }

    public void Cleanup()
    {
      foreach (GameEntity mousePosition in _mousePositions.GetEntities(_buffer))
      {
        mousePosition.isDestructed = true;
      }
    }
  }
}
