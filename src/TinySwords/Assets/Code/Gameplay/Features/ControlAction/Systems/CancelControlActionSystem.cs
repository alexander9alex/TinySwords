using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.UI.Hud.Service;
using Entitas;

namespace Code.Gameplay.Features.ControlAction.Systems
{
  public class CancelControlActionSystem : IExecuteSystem
  {
    private readonly IHudService _hudService;
    private readonly IInputService _inputService;

    private readonly IGroup<GameEntity> _cancelActionRequests;
    private readonly List<GameEntity> _fastInteractionBuffer = new(1);

    private readonly IGroup<GameEntity> _selectedActions;
    private readonly List<GameEntity> _selectedActionsBuffer = new(1);

    public CancelControlActionSystem(GameContext game, IHudService hudService, IInputService inputService)
    {
      _hudService = hudService;
      _inputService = inputService;

      _cancelActionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CancelControlAction));
      
      _selectedActions = game.GetGroup(GameMatcher.SelectedAction);
    }

    public void Execute()
    {
      foreach (GameEntity selectedAction in _selectedActions.GetEntities(_selectedActionsBuffer))
      foreach (GameEntity fastInteraction in _cancelActionRequests.GetEntities(_fastInteractionBuffer))
      {
        selectedAction.isDestructed = true;
        _hudService.CancelAction();
        _inputService.ChangeInputMap(InputMap.Game);

        CreateEntity.Empty()
          .With(x => x.isUpdateHudControlButtons = true);
        
        fastInteraction.isDestructed = true;
      }
    }
  }
}
