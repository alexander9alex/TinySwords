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

    public CommandService(IPhysicsService physicsService, ICameraProvider cameraProvider, IHudService hudService, IInputService inputService,
      ISoundService soundService, IBattleFormationService battleFormationService)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;
      _hudService = hudService;
      _inputService = inputService;
      _soundService = soundService;
      _battleFormationService = battleFormationService;
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

    public bool CanProcessAimedAttack(out GameEntity target, Vector2 screenPos)
    {
      target = null;

      List<GameEntity> targets = GetTargetsToAimedAttack(screenPos);

      foreach (GameEntity possibleTarget in targets)
      {
        if (TargetIsNotSuitable(possibleTarget))
          continue;

        target = possibleTarget;
        return true;
      }

      return false;
    }

    public void ProcessIncorrectAimedAttack(Vector2 screenPos)
    {
      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.IncorrectCommand)
        .AddScreenPosition(screenPos)
        .With(x => x.isCreateIndicator = true);
    }

    public void ProcessMoveCommand(GameEntity request, IGroup<GameEntity> selected) =>
      ProcessMoveCommand(selected, request.ScreenPosition, GetMoveUserCommand);

    public void ProcessMoveWithAttackCommand(GameEntity request, IGroup<GameEntity> selected) =>
      ProcessMoveCommand(selected, request.ScreenPosition, GetMoveWithAttackUserCommand);

    public void ProcessAimedAttack(GameEntity request, IGroup<GameEntity> selected)
    {
      if (!CanProcessAimedAttack(out GameEntity target, request.ScreenPosition))
        return;

      foreach (GameEntity entity in selected.GetEntities(_selectedBuffer))
      {
        entity.ReplaceUserCommand(GetAimedAttackUserCommand(target.Id));
        entity.isMakeDecisionNowRequest = true;
      }

      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.Attack)
        .AddWorldPosition(target.WorldPosition)
        .AddTargetId(target.Id)
        .With(x => x.isCreateIndicator = true);
    }

    private void ProcessMoveCommand(IGroup<GameEntity> selected, Vector2 screenPos, Func<Vector2, UserCommand> getUserCommand)
    {
      List<Vector2> battleFormationPositions = _battleFormationService
        .GetSquareBattleFormation(WorldPosition(screenPos), selected.count)
        .ToList();

      foreach (GameEntity entity in selected.GetEntities(_selectedBuffer))
      {
        entity.ReplaceUserCommand(getUserCommand(battleFormationPositions[0]));
        battleFormationPositions.RemoveAt(0);

        entity.isMakeDecisionNowRequest = true;
      }

      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.Move)
        .AddScreenPosition(screenPos)
        .With(x => x.isCreateIndicator = true);
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

    private List<GameEntity> GetTargetsToAimedAttack(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.MainCamera.ScreenToWorldPoint(mousePos),
          GameConstants.ClickRadius, GameConstants.UnitsAndBuildingsLayerMask)
        .OrderBy(entity => entity.Transform.position.y)
        .ToList();
    }

    private static UserCommand GetMoveWithAttackUserCommand(Vector2 pos)
    {
      return new UserCommand
      {
        CommandTypeId = CommandTypeId.MoveWithAttack,
        WorldPosition = pos
      };
    }

    private static UserCommand GetMoveUserCommand(Vector2 pos)
    {
      return new UserCommand
      {
        CommandTypeId = CommandTypeId.Move,
        WorldPosition = pos
      };
    }

    private static UserCommand GetAimedAttackUserCommand(int targetId)
    {
      return new UserCommand
      {
        CommandTypeId = CommandTypeId.AimedAttack,
        TargetId = targetId
      };
    }

    private Vector3 WorldPosition(Vector2 screenPos) =>
      _cameraProvider.MainCamera.ScreenToWorldPoint(screenPos);

    private bool CanApplyAimedAttackCommand(Vector2 screenPos) =>
      CanProcessAimedAttack(out _, screenPos);

    private static bool TargetIsNotSuitable(GameEntity possibleTarget) =>
      !TargetIsSuitable(possibleTarget);

    private static bool TargetIsSuitable(GameEntity target) =>
      target.hasId && target.hasTransform && target.hasWorldPosition && target.hasTeamColor && target.TeamColor != GameConstants.UserTeamColor;
  }
}
