using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Features.Units.Data;
using Code.UI.Hud.Service;
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

    public CommandService(IPhysicsService physicsService, ICameraProvider cameraProvider, IHudService hudService, IInputService inputService,
      ISoundService soundService)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;
      _hudService = hudService;
      _inputService = inputService;
      _soundService = soundService;
    }

    public void SelectCommand(GameEntity command)
    {
      _hudService.SelectCommand(command.CommandTypeId);
      _inputService.ChangeInputMap(InputMap.CommandIsActive);
      _soundService.PlaySound(SoundId.SelectCommand);
      
      command.isSelectedCommand = true;
    }

    public void CancelCommand(GameEntity command)
    {
      _hudService.CancelCommand();
      _inputService.ChangeInputMap(InputMap.Game);

      CreateEntity.Empty()
        .With(x => x.isUpdateHudControlButtons = true);

      if (!command.isProcessed)
        _soundService.PlaySound(SoundId.CancelCommand);
    }

    public bool CanApplyCommand(GameEntity command, GameEntity request)
    {
      switch (command.CommandTypeId)
      {
        case CommandTypeId.Move:
        case CommandTypeId.MoveWithAttack:
          return true;
        case CommandTypeId.AimedAttack:
          return CanApplyAimedAttackCommand(request);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void ApplyCommand(GameEntity command, GameEntity request)
    {
      GameEntity entity = CreateEntity.Empty()
        .AddCommandTypeId(command.CommandTypeId)
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isProcessCommandRequest = true);

      SetCommandType(entity, command.CommandTypeId);

      _soundService.PlaySound(SoundId.ApplyCommand);

      command.isProcessed = true;

      CreateEntity.Empty()
        .With(x => x.isCancelCommand = true);
    }

    public void IncorrectCommand(GameEntity command, GameEntity request)
    {
      GameEntity entity = CreateEntity.Empty()
        .AddCommandTypeId(command.CommandTypeId)
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isProcessIncorrectCommandRequest = true);

      SetCommandType(entity, command.CommandTypeId);

      _soundService.PlaySound(SoundId.IncorrectCommand);
    }

    public bool CanProcessAimedAttack(out GameEntity target, GameEntity request)
    {
      target = null;

      List<GameEntity> targets = GetTargetsToAimedAttack(request.PositionOnScreen);

      foreach (GameEntity possibleTarget in targets)
      {
        if (TargetIsNotSuitable(possibleTarget))
          continue;

        target = possibleTarget;
        return true;
      }

      return false;
    }

    public void ProcessAimedAttack(GameEntity request, GameEntity selected, GameEntity target)
    {
      RemovePreviousCommand(selected);
      selected.ReplaceCommandTypeId(request.CommandTypeId);
      selected.ReplaceAimedTargetId(target.Id);
    }

    public bool CommandCompletedOnSelectable(GameEntity selectable) =>
      _selectableCommandService.CommandCompleted(selectable);

    private static void RemovePreviousCommand(GameEntity selected)
    {
      if (!selected.hasCommandTypeId)
        return;

      CreateEntity.Empty()
        .AddCommandTypeId(selected.CommandTypeId)
        .With(x => x.isRemovePreviousCommand = true);
    }

    private static void SetCommandType(GameEntity entity, CommandTypeId commandTypeId)
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

    private List<GameEntity> GetTargetsToAimedAttack(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.MainCamera.ScreenToWorldPoint(mousePos),
          GameConstants.ClickRadius, GameConstants.UnitsAndBuildingsLayerMask)
        .OrderBy(entity => entity.Transform.position.y)
        .ToList();
    }

    private bool CanApplyAimedAttackCommand(GameEntity request) =>
      CanProcessAimedAttack(out _, request);

    private static bool TargetIsNotSuitable(GameEntity possibleTarget) =>
      !TargetIsSuitable(possibleTarget);

    private static bool TargetIsSuitable(GameEntity target) =>
      target.hasId && target.hasTransform && target.hasWorldPosition && target.hasTeamColor && target.TeamColor != TeamColor.Blue;
  }
}
