using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Services;
using Code.Gameplay.Features.Indicators.Data;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessAimedAttackCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _processCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _selectedBuffer = new(32);
    private readonly ICommandService _commandService;

    public ProcessAimedAttackCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _processCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommandRequest, GameMatcher.AimedAttackCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));

      _selected = game.GetGroup(GameMatcher.AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processCommandRequests.GetEntities(_requestsBuffer))
      {
        ProcessAimedAttack(request);
      }
    }

    private void ProcessAimedAttack(GameEntity request)
    {
      if (!_commandService.CanProcessAimedAttack(out GameEntity target, request))
        return;

      foreach (GameEntity selected in _selected.GetEntities(_selectedBuffer))
        _commandService.ProcessAimedAttack(request, selected, target);
        
      CreateEntity.Empty()
        .AddWorldPosition(target.WorldPosition)
        .With(x => x.isChangeEndDestinationRequest = true);

      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.Attack)
        .AddWorldPosition(target.WorldPosition)
        .AddTargetId(target.Id)
        .With(x => x.isCreateIndicator = true);
    }
  }
}
