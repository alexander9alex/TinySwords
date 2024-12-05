using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessMoveWithAttackCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _processCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _selectedBuffer = new(32);

    public ProcessMoveWithAttackCommandSystem(GameContext game)
    {
      _processCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommandRequest, GameMatcher.MoveWithAttackCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));

      _selected = game.GetGroup(GameMatcher.AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processCommandRequests.GetEntities(_requestsBuffer))
      {
        ProcessCommand(request);
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
        .With(x => x.isCreateMoveIndicator = true);
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
