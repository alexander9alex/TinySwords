using System.Collections.Generic;
using Code.Gameplay.Features.Command.Data;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class RemovePreviousCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _removePreviousCommandRequests;
    private readonly List<GameEntity> _buffer = new(32);
    
    private readonly IGroup<GameEntity> _selected;
    
    public RemovePreviousCommandSystem(GameContext game)
    {
      _removePreviousCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.RemovePreviousCommand, GameMatcher.CommandTypeId));
      
      _selected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _removePreviousCommandRequests.GetEntities(_buffer))
      {
        foreach (GameEntity selectable in _selected)
          RemovePreviousCommand(selectable, request);
        
        request.isDestructed = true;
      }
    }

    private void RemovePreviousCommand(GameEntity selectable, GameEntity request)
    {
      if (selectable.CommandTypeId == request.CommandTypeId)
        return;

      if (request.CommandTypeId == CommandTypeId.AimedAttack && selectable.hasAimedTargetId)
        selectable.RemoveAimedTargetId();
    }
  }
}
