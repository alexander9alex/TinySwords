using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.UI.Hud.Service;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class SelectCommandSystem : IExecuteSystem
  {
    private readonly IHudService _hudService;
    private readonly IInputService _inputService;

    private readonly IGroup<GameEntity> _commands;
    private readonly List<GameEntity> _buffer = new(1);

    public SelectCommandSystem(GameContext game, IHudService hudService, IInputService inputService)
    {
      _hudService = hudService;
      _inputService = inputService;

      _commands = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CommandTypeId)
        .NoneOf(GameMatcher.SelectedCommand));
    }

    public void Execute()
    {
      foreach (GameEntity unitAction in _commands.GetEntities(_buffer))
      {
        _hudService.SelectCommand(unitAction.CommandTypeId);
        _inputService.ChangeInputMap(InputMap.CommandIsActive);

        unitAction.With(x => x.isSelectedCommand = true);
      }
    }
  }
}
