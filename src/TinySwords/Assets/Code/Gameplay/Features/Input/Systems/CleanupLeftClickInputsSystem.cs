using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupLeftClickInputsSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _leftClickEnded;
    private readonly IGroup<GameEntity> _leftClickStarted;
    private readonly List<GameEntity> _endedBuffer = new(1);
    private readonly List<GameEntity> _startedBuffer = new(1);

    public CleanupLeftClickInputsSystem(GameContext game)
    {
      _leftClickEnded = game.GetGroup(GameMatcher.AllOf(GameMatcher.LeftClickEnded));
      _leftClickStarted = game.GetGroup(GameMatcher.AllOf(GameMatcher.LeftClickStarted));
    }

    public void Cleanup()
    {
      foreach (GameEntity ended in _leftClickEnded.GetEntities(_endedBuffer))
      foreach (GameEntity started in _leftClickStarted.GetEntities(_startedBuffer))
      {
        started.isDestructed = true;
        ended.isDestructed = true;
      }
    }
  }
}
