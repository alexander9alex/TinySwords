using System.Collections.Generic;
using Code.Gameplay.Features.Cameras.Services;
using Entitas;

namespace Code.Gameplay.Features.Cameras.Systems
{
  public class MoveCameraSystem : IExecuteSystem
  {
    private readonly ICameraMovementService _cameraMovementService;

    private readonly IGroup<GameEntity> _moveCameraRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public MoveCameraSystem(GameContext game, ICameraMovementService cameraMovementService)
    {
      _cameraMovementService = cameraMovementService;
      _moveCameraRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveCamera, GameMatcher.MoveDirection));
    }

    public void Execute()
    {
      foreach (GameEntity request in _moveCameraRequests.GetEntities(_buffer))
      {
        _cameraMovementService.MoveCamera(request.MoveDirection);

        request.isDestructed = true;
      }
    }
  }
}
