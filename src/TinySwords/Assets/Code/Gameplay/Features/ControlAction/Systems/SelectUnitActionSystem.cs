using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.UI.Hud.Service;
using Entitas;

namespace Code.Gameplay.Features.ControlAction.Systems
{
  public class SelectUnitActionSystem : IExecuteSystem
  {
    private readonly IHudService _hudService;
    private readonly IInputService _inputService;

    private readonly IGroup<GameEntity> _unitActions;
    private readonly List<GameEntity> _buffer = new(1);

    public SelectUnitActionSystem(GameContext game, IHudService hudService, IInputService inputService)
    {
      _hudService = hudService;
      _inputService = inputService;
      _unitActions = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.UnitActionTypeId)
        .NoneOf(GameMatcher.SelectedAction));
    }

    public void Execute()
    {
      foreach (GameEntity unitAction in _unitActions.GetEntities(_buffer))
      {
        _hudService.SetAction(unitAction.UnitActionTypeId);
        _inputService.ChangeInputMap(InputMap.ActionIsActive);
        
        unitAction.With(x => x.isSelectedAction = true);
      }
    }
  }
}
