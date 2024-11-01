using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Selecting.Systems
{
  public class CleanupSelectedNowSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _selectedNow;
    private readonly List<GameEntity> _buffer = new(64);

    public CleanupSelectedNowSystem(GameContext game)
    {
      _selectedNow = game.GetGroup(GameMatcher.AllOf(GameMatcher.SelectedNow));
    }

    public void Cleanup()
    {
      foreach (GameEntity selectedNow in _selectedNow.GetEntities(_buffer))
      {
        selectedNow.isSelectedNow = false;
      }
    }
  }
}
