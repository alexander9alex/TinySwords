using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessAimedAttackCommandSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;
    private readonly GameContext _game;

    private readonly IGroup<GameEntity> _updateCommandRequests;
    private readonly List<GameEntity> _requestsBuffer = new(1);

    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _selectedBuffer = new(32);

    public ProcessAimedAttackCommandSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _game = game;
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _updateCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessCommand, GameMatcher.AimedAttackCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));

      _selected = game.GetGroup(GameMatcher.AllOf(GameMatcher.Selected, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _updateCommandRequests.GetEntities(_requestsBuffer))
      {
        ProcessCommand(request);
        request.isDestructed = true;
      }
    }

    private void ProcessCommand(GameEntity request)
    {
      List<int> targetIds = GetTargetsFromRadius(request.PositionOnScreen);

      if (targetIds.Count == 0)
        return;

      GameEntity firstTarget = _game.GetEntityWithId(targetIds[0]);

      if (!firstTarget.hasWorldPosition)
        return;
      
      foreach (GameEntity selected in _selected.GetEntities(_selectedBuffer))
        ProcessCommand(request, selected, firstTarget.Id);

      CreateEntity.Empty()
        .AddWorldPosition(firstTarget.WorldPosition)
        .With(x => x.isChangeEndDestinationRequest = true);

      // CreateEntity.Empty()
      // .AddPositionOnScreen(request.PositionOnScreen)
      // .With(x => x.isCreateMoveClickIndicator = true);
      // todo: create red indicator (?)
    }

    private static void ProcessCommand(GameEntity request, GameEntity selected, int targetId)
    {
      RemovePreviousCommand(selected);
      selected.ReplaceCommandTypeId(request.CommandTypeId);
      selected.ReplaceAimedTargetId(targetId);
    }

    private static void RemovePreviousCommand(GameEntity selected)
    {
      if (!selected.hasCommandTypeId)
        return;
      
      CreateEntity.Empty()
        .AddCommandTypeId(selected.CommandTypeId)
        .With(x => x.isRemovePreviousCommand = true);
    }

    private List<int> GetTargetsFromRadius(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.MainCamera.ScreenToWorldPoint(mousePos),
          GameConstants.ClickRadius, GameConstants.UnitsAndBuildingsLayerMask)
        .Where(entity => entity.hasTransform)
        .Where(entity => entity.hasId)
        .OrderBy(entity => entity.Transform.position.y)
        .Select(entity => entity.Id)
        .ToList();
    }
  }
}
