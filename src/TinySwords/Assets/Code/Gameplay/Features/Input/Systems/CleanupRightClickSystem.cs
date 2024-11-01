using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupRightClickSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _rightClicks;
    private readonly List<GameEntity> _clicksBuffer = new(1);

    public CleanupRightClickSystem(GameContext game)
    {
      _rightClicks = game.GetGroup(GameMatcher.AllOf(GameMatcher.RightClick));
    }

    public void Cleanup()
    {
      foreach (GameEntity click in _rightClicks.GetEntities(_clicksBuffer))
      {
        click.isDestructed = true;
      }
    }
  }
}
