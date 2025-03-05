using System.Collections.Generic;
using Code.Gameplay.Features.Cameras.Services;
using Entitas;

namespace Code.Gameplay.Features.Cameras.Systems
{
  public class MoveCameraByInputSystem : IExecuteSystem
  {
    private readonly ICameraMovementService _cameraMovementService;

    private readonly IGroup<GameEntity> _inputs;
    private readonly List<GameEntity> _buffer = new(1);

    public MoveCameraByInputSystem(GameContext game, ICameraMovementService cameraMovementService)
    {
      _cameraMovementService = cameraMovementService;

      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.MoveCameraDirection
        ));
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs.GetEntities(_buffer))
      {
        _cameraMovementService.MoveCamera(input.MoveCameraDirection);

        input.RemoveMoveCameraDirection();
      }
    }
  }
}
