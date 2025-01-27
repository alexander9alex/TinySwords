using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Cameras.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Systems
{
  public class ScaleCameraSystem : IExecuteSystem
  {
    private readonly ICameraScalingService _cameraScalingService;
    private readonly IGroup<GameEntity> _scaleCameraRequests;
    private readonly List<GameEntity> _buffer = new(4);

    public ScaleCameraSystem(GameContext contextParameter, ICameraScalingService cameraScalingService)
    {
      _cameraScalingService = cameraScalingService;
      _scaleCameraRequests = contextParameter.GetGroup(GameMatcher.ScaleCamera);
    }

    public void Execute()
    {
      foreach (GameEntity request in _scaleCameraRequests.GetEntities(_buffer))
      {
        _cameraScalingService.ScaleCamera(request.ScaleCamera);

        request.isDestructed = true;
      }
    }
  }
}
