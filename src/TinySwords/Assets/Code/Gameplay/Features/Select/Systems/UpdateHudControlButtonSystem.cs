using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Constants;
using Code.UI.Buttons.Data;
using Code.UI.Hud.Service;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class UpdateHudControlButtonSystem : IExecuteSystem
  {
    private readonly IHudService _hudService;
    
    private readonly IGroup<GameEntity> _updateHudControlButtonsRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public UpdateHudControlButtonSystem(GameContext game, IHudService hudService)
    {
      _hudService = hudService;

      _updateHudControlButtonsRequests = game.GetGroup(GameMatcher.UpdateHudControlButtons);
      _selected = game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
      foreach (GameEntity request in _updateHudControlButtonsRequests.GetEntities(_buffer))
      {
        List<ActionTypeId> availableActions = new(GameConstants.AllActions);

        foreach (GameEntity selected in _selected)
        {
          if (!selected.hasAllActionTypeIds)
          {
            availableActions.Clear();
            break;
          }

          availableActions = availableActions.Intersect(selected.AllActionTypeIds).ToList();
        }

        _hudService.UpdateAvailableActions(availableActions);

        request.isDestructed = true;
      }
    }
  }
}
