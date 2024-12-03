using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Systems
{
  public class MoveCameraSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _moveCameraRequests;
    private readonly List<GameEntity> _buffer = new(1);
    private readonly ICameraProvider _cameraProvider;

    public MoveCameraSystem(GameContext game, ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
      _moveCameraRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveCamera, GameMatcher.MoveDirection));
    }

    public void Execute()
    {
      foreach (GameEntity request in _moveCameraRequests.GetEntities(_buffer))
      {
        _cameraProvider.MainCamera.transform.position += request.MoveDirection.ToVector3() * GameConstants.CameraSpeed;
        
        request.isDestructed = true;
      }
    }
  }
}
