using System.Collections.Generic;
using Code.Gameplay.Features.Cameras.Services;
using Code.Gameplay.Features.Input.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Systems
{
  public class MoveCameraByMousePositionSystem : IExecuteSystem
  {
    private readonly ICameraMovementService _cameraMovementService;
    private readonly IInputService _inputService;
    
    private readonly int _borderSize;
    private readonly Vector2 _screenSize;

    private readonly IGroup<GameEntity> _inputs;
    private readonly List<GameEntity> _buffer = new(1);

    public MoveCameraByMousePositionSystem(GameContext game, ICameraMovementService cameraMovementService, IInputService inputService)
    {
      _cameraMovementService = cameraMovementService;
      _inputService = inputService;

      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.MousePosition
        ));

      _borderSize = Screen.width / 10;
      _screenSize = new Vector2(Screen.width, Screen.height);
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs.GetEntities(_buffer))
      {
        if (CanMoveCamera(input.MousePosition))
          MoveCamera(input.MousePosition);
      }
    }

    private bool CanMoveCamera(Vector2 mousePos) =>
      _inputService.PositionInGameZone(mousePos) && GetMoveDir(mousePos) != Vector2.zero;

    private void MoveCamera(Vector2 mousePos) =>
      _cameraMovementService.MoveCamera(GetMoveDir(mousePos));

    private Vector2 GetMoveDir(Vector2 mousePos)
    {
      Vector2 moveDir;

      moveDir.x = GetAxisInput(mousePos.x, _screenSize.x);
      moveDir.y = GetAxisInput(mousePos.y, _screenSize.y);

      return moveDir;
    }

    private int GetAxisInput(float axis, float screenSizeAxis)
    {
      if (axis <= _borderSize)
        return -1;

      if (screenSizeAxis - axis <= _borderSize)
        return 1;

      return 0;
    }
  }
}
