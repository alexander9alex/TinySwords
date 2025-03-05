using System.Collections.Generic;
using Code.Gameplay.Features.Cameras.Services;
using Entitas;

namespace Code.Gameplay.Features.Cameras.Systems
{
  public class ScaleCameraSystem : IExecuteSystem
  {
    private readonly ICameraScalingService _cameraScalingService;

    private readonly List<GameEntity> _buffer = new(1);
    private readonly IGroup<GameEntity> _inputs;

    public ScaleCameraSystem(GameContext game, ICameraScalingService cameraScalingService)
    {
      _cameraScalingService = cameraScalingService;

      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.ScaleCamera
        ));
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs.GetEntities(_buffer))
      {
        _cameraScalingService.ScaleCamera(input.ScaleCamera);

        input.RemoveScaleCamera();
      }
    }
  }
}
