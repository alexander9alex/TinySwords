using Code.Gameplay.Level.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  public interface ICameraMovementService
  {
    void MoveCamera(Vector2 moveDir);
    void SetCameraBorders(LevelId levelId);
  }
}
