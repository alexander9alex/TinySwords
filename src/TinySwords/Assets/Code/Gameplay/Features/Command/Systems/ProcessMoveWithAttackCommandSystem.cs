using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Indicators.Data;
using Code.Gameplay.Features.Move.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessMoveWithAttackCommandSystem : IExecuteSystem
  {
    private readonly IBattleFormationService _battleFormationService;
    private readonly ICameraProvider _cameraProvider;
    
    private readonly IGroup<GameEntity> _processCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _selectedBuffer = new(32);

    public ProcessMoveWithAttackCommandSystem(GameContext game, ICameraProvider cameraProvider, IBattleFormationService battleFormationService)
    {
      _cameraProvider = cameraProvider;
      _battleFormationService = battleFormationService;

      _processCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommandRequest, GameMatcher.MoveWithAttackCommand, GameMatcher.CommandTypeId, GameMatcher.ScreenPosition));

      _selected = game.GetGroup(GameMatcher.AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processCommandRequests.GetEntities(_requestsBuffer))
      {
        ProcessCommand(request);
      }
    }

    private void ProcessCommand(GameEntity request)
    {
      List<Vector2> battleFormationPositions = _battleFormationService
        .GetSquareBattleFormation(WorldPosition(request), _selected.count)
        .ToList();
      
      foreach (GameEntity selected in _selected.GetEntities(_selectedBuffer))
      {
        ReplaceUserCommand(selected, battleFormationPositions[0]);
        battleFormationPositions.RemoveAt(0);
      }

      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.Move)
        .AddScreenPosition(request.ScreenPosition)
        .With(x => x.isCreateIndicator = true);
    }

    private static void ReplaceUserCommand(GameEntity selected, Vector2 pos)
    {
      selected.ReplaceUserCommand(GetMoveWithAttackUserCommand(pos));
      selected.isMakeDecisionNowRequest = true;
    }

    private static UserCommand GetMoveWithAttackUserCommand(Vector2 pos)
    {
      return new UserCommand()
      {
        CommandTypeId = CommandTypeId.MoveWithAttack,
        WorldPosition = pos
      };
    }

    private Vector3 WorldPosition(GameEntity request) =>
      _cameraProvider.MainCamera.ScreenToWorldPoint(request.ScreenPosition);
  }
}
