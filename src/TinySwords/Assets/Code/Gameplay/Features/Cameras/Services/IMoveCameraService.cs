using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  public interface IMoveCameraService
  {
    void MoveCamera(Vector2 moveDir);
  }
}
