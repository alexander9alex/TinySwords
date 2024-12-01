using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Command.Data;
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
        _hudService.UpdateAvailableCommands(GetAvailableCommands());

        request.isDestructed = true;
      }
    }

    private List<CommandTypeId> GetAvailableCommands()
    {
      if (_selected.count == 0)
        return new List<CommandTypeId>();
      
      List<CommandTypeId> availableCommand = new(GameConstants.AllUnitCommands);

      foreach (GameEntity selected in _selected)
      {
        if (!selected.hasAllUnitCommandTypeIds)
        {
          availableCommand.Clear();
          break;
        }

        availableCommand = availableCommand.Intersect(selected.AllUnitCommandTypeIds).ToList();
      }

      return availableCommand;
    }
  }
}
