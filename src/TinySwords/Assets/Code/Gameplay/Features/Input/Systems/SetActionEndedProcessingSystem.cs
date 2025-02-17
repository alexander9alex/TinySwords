using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class SetActionEndedProcessingSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _actionEnded;
    private readonly IGroup<GameEntity> _actionStarted;
    private readonly List<GameEntity> _buffer = new(1);

    public SetActionEndedProcessingSystem(GameContext game)
    {
      _actionEnded = game.GetGroup(GameMatcher.InteractionEnded);
      _actionStarted = game.GetGroup(GameMatcher.InteractionStarted);
    }

    public void Execute()
    {
      foreach (GameEntity ended in _actionEnded.GetEntities(_buffer))
      foreach (GameEntity started in _actionStarted)
      {
        ended.isProcessed = started.isProcessed;
      }
    }
  }
}
