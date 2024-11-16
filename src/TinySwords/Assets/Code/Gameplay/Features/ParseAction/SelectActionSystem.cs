using System.Collections.Generic;
using Code.Common.Extensions;
using Code.UI.Hud.Service;
using Entitas;

namespace Code.Gameplay.Features.ParseAction
{
  public class SelectActionSystem : IExecuteSystem
  {
    private readonly IHudService _hudService;
    
    private readonly IGroup<GameEntity> _moveActions;
    private readonly List<GameEntity> _buffer = new(1);

    public SelectActionSystem(GameContext game, IHudService hudService)
    {
      _hudService = hudService;
      _moveActions = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ActionTypeId)
        .NoneOf(GameMatcher.SelectedAction));
    }

    public void Execute()
    {
      foreach (GameEntity action in _moveActions.GetEntities(_buffer))
      {
        _hudService.SetAction(action.ActionTypeId);
        action.With(x => x.isSelectedAction = true);
      }
    }
  }
}
