using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupMousePositionSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _mousePositionInputs;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupMousePositionSystem(GameContext game)
    {
      _mousePositionInputs = game.GetGroup(GameMatcher.MousePositionOnScreen);
    }

    public void Cleanup()
    {
      foreach (GameEntity mousePosition in _mousePositionInputs.GetEntities(_buffer))
      {
        mousePosition.isDestructed = true;
      }
    }
  }
}
