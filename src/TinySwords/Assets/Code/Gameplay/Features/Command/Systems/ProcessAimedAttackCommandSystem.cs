using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessAimedAttackCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _updateCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _selectedBuffer = new(32);
    private readonly ICommandService _commandService;

    public ProcessAimedAttackCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _updateCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommand, GameMatcher.AimedAttackCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));

      _selected = game.GetGroup(GameMatcher.AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _updateCommandRequests.GetEntities(_requestsBuffer))
      {
        ProcessAimedAttack(request);
        request.isDestructed = true;
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
        .AddWorldPosition(target.WorldPosition)
        .AddTargetId(target.Id)
        .With(x => x.isCreateAttackIndicator = true);
    }
  }
}
