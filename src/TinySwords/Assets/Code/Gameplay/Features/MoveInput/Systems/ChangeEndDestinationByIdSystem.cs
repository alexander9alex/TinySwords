using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.MoveInput.Systems
{
  public class ChangeEndDestinationByIdSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    
    private readonly IGroup<GameEntity> _changeEndDestinationRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public ChangeEndDestinationByIdSystem(GameContext game)
    {
      _game = game;
      
      _changeEndDestinationRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ChangeEndDestinationRequest, GameMatcher.WorldPosition, GameMatcher.TargetId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _changeEndDestinationRequests.GetEntities(_buffer))
      {
        GameEntity target = _game.GetEntityWithId(request.TargetId);

        if (!target.isSelected || !target.isMovable || !target.isAlive)
          return;

        target.ReplaceEndDestination(request.WorldPosition);
        target.ReplaceMakeDecisionTimer(0);
      }
    }
  }
}
