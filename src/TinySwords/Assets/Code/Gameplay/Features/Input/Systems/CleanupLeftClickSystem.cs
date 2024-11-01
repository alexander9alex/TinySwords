using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupLeftClickSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _leftClicks;
    private readonly List<GameEntity> _clicksBuffer = new(1);

    public CleanupLeftClickSystem(GameContext game)
    {
      _leftClicks = game.GetGroup(GameMatcher.AllOf(GameMatcher.LeftClick));
    }

    public void Cleanup()
    {
      foreach (GameEntity click in _leftClicks.GetEntities(_clicksBuffer))
      {
        click.isDestructed = true;
      }
    }
  }
}
