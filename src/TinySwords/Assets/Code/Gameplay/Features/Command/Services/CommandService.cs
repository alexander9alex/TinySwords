using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Services
{
  class CommandService : ICommandService
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    public CommandService(IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;
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
        .With(x => x.isProcessCommand = true);

      switch (command.CommandTypeId)
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

      CreateEntity.Empty()
        .With(x => x.isCancelCommand = true);
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

    private static void RemovePreviousCommand(GameEntity selected)
    {
      if (!selected.hasCommandTypeId)
        return;

      CreateEntity.Empty()
        .AddCommandTypeId(selected.CommandTypeId)
        .With(x => x.isRemovePreviousCommand = true);
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
