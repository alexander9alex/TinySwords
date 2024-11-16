using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class CleanupSelectedChangedRequestSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _selectedChanged;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupSelectedChangedRequestSystem(GameContext game)
    {
      _selectedChanged = game.GetGroup(GameMatcher.SelectedChanged);
    }

    public void Cleanup()
    {
      foreach (GameEntity request in _selectedChanged.GetEntities(_buffer))
      {
        request.isDestructed = true;
      }
    }
  }
}
