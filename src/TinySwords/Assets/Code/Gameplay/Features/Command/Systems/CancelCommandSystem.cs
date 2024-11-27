using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.UI.Hud.Service;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class CancelCommandSystem : IExecuteSystem
  {
    private readonly IHudService _hudService;
    private readonly IInputService _inputService;

    private readonly IGroup<GameEntity> _cancelCommandRequests;
    private readonly List<GameEntity> _cancelCommandBuffer = new(1);

    private readonly IGroup<GameEntity> _selectedCommands;
    private readonly List<GameEntity> _selectedCommandsBuffer = new(1);

    public CancelCommandSystem(GameContext game, IHudService hudService, IInputService inputService)
    {
      _hudService = hudService;
      _inputService = inputService;

      _cancelCommandRequests = game.GetGroup(GameMatcher.CancelCommand);
      _selectedCommands = game.GetGroup(GameMatcher.AllOf(GameMatcher.Command, GameMatcher.SelectedCommand));
    }

    public void Execute()
    {
      foreach (GameEntity request in _cancelCommandRequests.GetEntities(_cancelCommandBuffer))
      foreach (GameEntity command in _selectedCommands.GetEntities(_selectedCommandsBuffer))
      {
        _hudService.CancelCommand();
        _inputService.ChangeInputMap(InputMap.Game);

        CreateEntity.Empty()
          .With(x => x.isUpdateHudControlButtons = true);
        
        command.isDestructed = true;
        request.isDestructed = true;
      }
    }
  }
}
