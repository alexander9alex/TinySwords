﻿using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Indicators.Data;
using Code.Gameplay.Features.Move.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ProcessCommand.Services
{
  class ProcessCommandService : IProcessCommandService
  {
    private readonly IBattleFormationService _battleFormationService;
    private readonly ICameraProvider _cameraProvider;
    private readonly IPhysicsService _physicsService;

    private readonly List<GameEntity> _selectedBuffer = new(32);

    public ProcessCommandService(IBattleFormationService battleFormationService, ICameraProvider cameraProvider, IPhysicsService physicsService)
    {
      _battleFormationService = battleFormationService;
      _cameraProvider = cameraProvider;
      _physicsService = physicsService;
    }

    public void ProcessMoveCommand(GameEntity request, IGroup<GameEntity> selected) =>
      ProcessMoveCommand(selected, request.ScreenPosition, GetMoveUserCommand);

    public void ProcessMoveWithAttackCommand(GameEntity request, IGroup<GameEntity> selected) =>
      ProcessMoveCommand(selected, request.ScreenPosition, GetMoveWithAttackUserCommand);

    public void ProcessAimedAttack(GameEntity request, IGroup<GameEntity> selected)
    {
      if (!HasHostileTargetInScreenPosition(out GameEntity target, request.ScreenPosition))
        return;

      foreach (GameEntity entity in selected.GetEntities(_selectedBuffer))
      {
        entity.ReplaceUserCommand(GetAimedAttackUserCommand(target.Id));
        entity.isUpdateFieldOfVisionNowRequest = true;
      }

      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.Attack)
        .AddWorldPosition(target.WorldPosition)
        .AddTargetId(target.Id)
        .With(x => x.isCreateIndicator = true);
    }

    public void ProcessIncorrectCommand(Vector2 screenPos)
    {
      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.IncorrectCommand)
        .AddScreenPosition(screenPos)
        .With(x => x.isCreateIndicator = true);
    }

    public bool CanProcessAimedAttack(Vector2 screenPos) =>
      HasHostileTargetInScreenPosition(out _, screenPos);

    private bool HasHostileTargetInScreenPosition(out GameEntity target, Vector2 screenPos)
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

    private void ProcessMoveCommand(IGroup<GameEntity> selected, Vector2 screenPos, Func<Vector2, UserCommand> getUserCommand)
    {
      List<Vector2> battleFormationPositions = _battleFormationService
        .GetRectangleBattleFormation(WorldPosition(screenPos), selected.count)
        .ToList();

      foreach (GameEntity entity in selected.GetEntities(_selectedBuffer))
      {
        entity.ReplaceUserCommand(getUserCommand(battleFormationPositions[0]));
        battleFormationPositions.RemoveAt(0);

        entity.isUpdateFieldOfVisionNowRequest = true;
        entity.isOffsetPositionByLegs = true;
      }

      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.Move)
        .AddScreenPosition(screenPos)
        .With(x => x.isCreateIndicator = true);
    }

    private List<GameEntity> GetTargetsToAimedAttack(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.ScreenToWorldPoint(mousePos),
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
      _cameraProvider.ScreenToWorldPoint(screenPos);

    private static bool TargetIsNotSuitable(GameEntity possibleTarget) =>
      !TargetIsSuitable(possibleTarget);

    private static bool TargetIsSuitable(GameEntity target) =>
      target.hasId && target.hasTransform && target.hasWorldPosition && target.hasTeamColor && target.TeamColor == GameConstants.HostileEnemyColor;
  }
}
