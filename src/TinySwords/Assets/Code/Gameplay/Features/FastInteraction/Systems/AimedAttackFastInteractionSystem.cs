using System.Collections.Generic;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.FastInteraction.Systems
{
  public class AimedAttackFastInteractionSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;
    
    private readonly IGroup<GameEntity> _fastInteractionRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public AimedAttackFastInteractionSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _fastInteractionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FastInteraction, GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
      foreach (GameEntity request in _fastInteractionRequests.GetEntities(_buffer))
      {
        if (CanNotMakeAimedAttack(request))
          return;

        _commandService.CreateProcessCommandRequest(CommandTypeId.AimedAttack, request);

        request.isProcessed = true;
      }
    }

    private bool AllSelectedCanAttack()
    {
      foreach (GameEntity selected in _selected)
      {
        if (!selected.isCanAttack)
          return false;
      }

      return true;
    }

    private bool CanNotMakeAimedAttack(GameEntity request) =>
      !_commandService.CanApplyCommand(CommandTypeId.AimedAttack, request) || !SelectedCanAttack();

    private bool SelectedCanAttack() =>
      HasSelected() && AllSelectedCanAttack();

    private bool HasSelected() =>
      _selected.count > 0;
  }
}
