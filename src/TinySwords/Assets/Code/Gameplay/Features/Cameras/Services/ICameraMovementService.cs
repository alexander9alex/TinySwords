﻿using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  public interface ICameraMovementService
  {
    void MoveCamera(Vector2 moveDir);
    void SetCameraBorders(LevelConfig config);
    void RecalculateCameraPosition();
  }
}
