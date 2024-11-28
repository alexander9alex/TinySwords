using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessMoveCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _updateCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _selectedBuffer = new(32);

    public ProcessMoveCommandSystem(GameContext game)
    {
      _updateCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommand, GameMatcher.MoveCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));

      _selected = game.GetGroup(GameMatcher.AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _updateCommandRequests.GetEntities(_requestsBuffer))
      {
        ProcessCommand(request);
        request.isDestructed = true;
      }
    }

    private void ProcessCommand(GameEntity request)
    {
      foreach (GameEntity selected in _selected.GetEntities(_selectedBuffer))
        ProcessCommand(selected, request);

      CreateEntity.Empty()
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isChangeEndDestinationRequest = true)
        .With(x => x.isConvertWhenGroup = true);

      CreateEntity.Empty()
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isCreateMoveClickIndicator = true);
    }

    private static void ProcessCommand(GameEntity selected, GameEntity request)
    {
      RemovePreviousCommand(selected);      
      selected.ReplaceCommandTypeId(request.CommandTypeId);
    }
    
    private static void RemovePreviousCommand(GameEntity selected)
    {
      if (!selected.hasCommandTypeId)
        return;
      
      CreateEntity.Empty()
        .AddCommandTypeId(selected.CommandTypeId)
        .With(x => x.isRemovePreviousCommand = true);
    }
  }
}
