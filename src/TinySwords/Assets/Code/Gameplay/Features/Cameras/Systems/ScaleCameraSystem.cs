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
    private readonly IScaleCameraService _scaleCameraService;
    private readonly IGroup<GameEntity> _scaleCameraInputs;
    private readonly List<GameEntity> _buffer = new(4);

    public ScaleCameraSystem(GameContext contextParameter, IScaleCameraService scaleCameraService)
    {
      _scaleCameraService = scaleCameraService;
      _scaleCameraInputs = contextParameter.GetGroup(GameMatcher.ScaleCamera);
    }

    public void Execute()
    {
      foreach (GameEntity input in _scaleCameraInputs.GetEntities(_buffer))
      {
        _scaleCameraService.ScaleCamera(input.ScaleCamera);

        CreateEntity.Empty()
          .AddMoveDirection(Vector2.zero)
          .With(x => x.isMoveCamera = true);
        
        input.isDestructed = true;
      }
    }
  }
}
