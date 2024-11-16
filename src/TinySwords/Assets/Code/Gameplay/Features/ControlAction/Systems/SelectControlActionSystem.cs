using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.UI.Hud.Service;
using Entitas;

namespace Code.Gameplay.Features.ControlAction.Systems
{
  public class SelectControlActionSystem : IExecuteSystem
  {
    private readonly IHudService _hudService;
    private readonly IInputService _inputService;

    private readonly IGroup<GameEntity> _moveActions;
    private readonly List<GameEntity> _buffer = new(1);

    public SelectControlActionSystem(GameContext game, IHudService hudService, IInputService inputService)
    {
      _hudService = hudService;
      _inputService = inputService;
      _moveActions = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ActionTypeId)
        .NoneOf(GameMatcher.SelectedAction));
    }

    public void Execute()
    {
      foreach (GameEntity action in _moveActions.GetEntities(_buffer))
      {
        _hudService.SetAction(action.ControlActionTypeId);
        _inputService.ChangeInputMap(InputMap.ActionIsActive);
        
        action.With(x => x.isSelectedAction = true);
      }
    }
  }
}
