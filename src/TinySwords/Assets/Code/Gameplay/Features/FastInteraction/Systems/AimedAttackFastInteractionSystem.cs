using System.Collections.Generic;
using Code.Gameplay.Features.FastInteraction.Services;
using Entitas;

namespace Code.Gameplay.Features.FastInteraction.Systems
{
  public class AimedAttackFastInteractionSystem : IExecuteSystem
  {
    private readonly IFastInteractionService _fastInteractionService;

    private readonly IGroup<GameEntity> _fastInteractionRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public AimedAttackFastInteractionSystem(GameContext game, IFastInteractionService fastInteractionService)
    {
      _fastInteractionService = fastInteractionService;

      _fastInteractionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FastInteraction, GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
      foreach (GameEntity request in _fastInteractionRequests.GetEntities(_buffer))
      {
        if (_fastInteractionService.HasNotTargetInPosition(request) || SelectedCanNotAttackAimed())
          return;

        _fastInteractionService.MakeAimedAttack(request);

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

    private bool SelectedCanNotAttackAimed() =>
      !CanAttackAimed();

    private bool CanAttackAimed() =>
      HasSelected() && AllSelectedCanAttack();

    private bool HasSelected() =>
      _selected.count > 0;
  }
}
