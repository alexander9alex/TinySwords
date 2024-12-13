using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Indicators.Data;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Move.Services;
using Code.Gameplay.Features.ProcessCommand.Services;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.UI.Hud.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Services
{
  class CommandService : ICommandService
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;
    private readonly IHudService _hudService;
    private readonly IInputService _inputService;
    private readonly ISoundService _soundService;
    private readonly SelectableCommandService _selectableCommandService;
    private readonly IBattleFormationService _battleFormationService;
    private readonly List<GameEntity> _selectedBuffer = new(32);
    private readonly IProcessCommandService _processCommandService;

    public CommandService(IPhysicsService physicsService, ICameraProvider cameraProvider, IHudService hudService, IInputService inputService,
      ISoundService soundService, IBattleFormationService battleFormationService, IProcessCommandService processCommandService)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;
      _hudService = hudService;
      _inputService = inputService;
      _soundService = soundService;
      _battleFormationService = battleFormationService;
      _processCommandService = processCommandService;
    }

    public void SelectCommand(CommandTypeId command)
    {
      _hudService.SelectCommand(command);
      _inputService.ChangeInputMap(InputMap.CommandIsActive);
      _soundService.PlaySound(SoundId.SelectCommand);
    }

    public void CancelCommand(bool isCommandProcessed)
    {
      _hudService.CancelCommand();
      _inputService.ChangeInputMap(InputMap.Game);

      CreateEntity.Empty()
        .With(x => x.isUpdateHudControlButtons = true);

      if (!isCommandProcessed)
        _soundService.PlaySound(SoundId.CancelCommand);
    }

    public bool CanApplyCommand(CommandTypeId command, Vector2 screenPos)
    {
      switch (command)
      {
        case CommandTypeId.Move:
        case CommandTypeId.MoveWithAttack:
          return true;
        case CommandTypeId.AimedAttack:
          return CanApplyAimedAttackCommand(screenPos);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void ApplyCommand(CommandTypeId command, Vector2 screenPos)
    {
      GameEntity entity = CreateEntity.Empty()
        .AddCommandTypeId(command)
        .AddScreenPosition(screenPos)
        .With(x => x.isProcessCommandRequest = true);

      SetCommandTypeId(entity, command);

      _soundService.PlaySound(SoundId.ApplyCommand);
    }

    public void ProcessIncorrectCommand(CommandTypeId command, GameEntity request)
    {
      GameEntity entity = CreateEntity.Empty()
        .AddCommandTypeId(command)
        .AddScreenPosition(request.ScreenPosition)
        .With(x => x.isProcessIncorrectCommandRequest = true);

      SetCommandTypeId(entity, command);

      _soundService.PlaySound(SoundId.IncorrectCommand);
    }

    

    private static void SetCommandTypeId(GameEntity entity, CommandTypeId commandTypeId)
    {
      switch (commandTypeId)
      {
        case CommandTypeId.Move:
          entity.isMoveCommand = true;
          break;
        case CommandTypeId.MoveWithAttack:
          entity.isMoveWithAttackCommand = true;
          break;
        case CommandTypeId.AimedAttack:
          entity.isAimedAttackCommand = true;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
    
    private bool CanApplyAimedAttackCommand(Vector2 screenPos) =>
      _processCommandService.CanProcessAimedAttack(out _, screenPos);
  }
}
