using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _updateCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _selectedBuffer = new(32);

    public ProcessCommandSystem(GameContext game)
    {
      _updateCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));

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
        selected.ReplaceCommandTypeId(request.CommandTypeId);
      
      CreateEntity.Empty()
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isChangeEndDestinationRequest = true);

      CreateEntity.Empty()
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isCreateMoveClickIndicator = true);
    }
  }
}
