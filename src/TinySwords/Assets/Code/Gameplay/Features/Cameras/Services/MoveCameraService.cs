using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Cameras.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  class MoveCameraService : IMoveCameraService
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly CameraConfig _cameraConfig;

    public MoveCameraService(ICameraProvider cameraProvider, IStaticDataService staticData)
    {
      _cameraProvider = cameraProvider;
      _cameraConfig = staticData.GetCameraConfig();
    }

    public void MoveCamera(Vector2 moveDir) =>
      _cameraProvider.MainCamera.transform.position += MoveVector(moveDir);

    private Vector3 MoveVector(Vector2 moveDir) =>
      moveDir.ToVector3() * _cameraConfig.MovementSpeed
      * (_cameraProvider.CameraScale / _cameraConfig.MaxScaling * _cameraConfig.CameraScaleOnMovementInfluenceCoefficient);
  }
}
