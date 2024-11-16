using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.UI.Hud.Service;
using Entitas;

namespace Code.Gameplay.Features.FastInteract.Systems
{
  public class CancelActionSystem : IExecuteSystem
  {
    private readonly IHudService _hudService;
    
    private readonly IGroup<GameEntity> _fastInteractions;
    private readonly List<GameEntity> _fastInteractionBuffer = new(1);

    private readonly IGroup<GameEntity> _selectedActions;
    private readonly List<GameEntity> _selectedActionsBuffer = new(1);

    public CancelActionSystem(GameContext game, IHudService hudService)
    {
      _hudService = hudService;
      _fastInteractions = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FastInteraction)
        .NoneOf(GameMatcher.Processed));
      
      _selectedActions = game.GetGroup(GameMatcher.SelectedAction);
    }

    public void Execute()
    {
      foreach (GameEntity selectedAction in _selectedActions.GetEntities(_selectedActionsBuffer))
      foreach (GameEntity fastInteraction in _fastInteractions.GetEntities(_fastInteractionBuffer))
      {
        _hudService.CancelAction();
        selectedAction.isDestructed = true;

        CreateEntity.Empty()
          .With(x => x.isUpdateHudControlButtons = true);
        
        fastInteraction.isProcessed = true;
      }
    }
  }
}
