using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupActionInputSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _selectionStarted;
    private readonly List<GameEntity> _startedBuffer = new(1);
    
    private readonly IGroup<GameEntity> _selectionEnded;
    private readonly List<GameEntity> _endedBuffer = new(1);

    public CleanupActionInputSystem(GameContext game)
    {
      _selectionStarted = game.GetGroup(GameMatcher.AllOf(GameMatcher.ActionStarted));
      _selectionEnded = game.GetGroup(GameMatcher.AllOf(GameMatcher.ActionEnded));
    }

    public void Cleanup()
    {
      foreach (GameEntity ended in _selectionEnded.GetEntities(_endedBuffer))
      foreach (GameEntity started in _selectionStarted.GetEntities(_startedBuffer))
      {
        started.isDestructed = true;
        ended.isDestructed = true;
      }
    }
  }
}
