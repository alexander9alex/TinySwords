using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Cameras.Configs;
using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Data;
using Code.Gameplay.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  class CameraMovementService : ICameraMovementService
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly CameraConfig _cameraConfig;
    private readonly ITimeService _time;

    private float CameraScaleInfluenceCoefficient =>
      _cameraProvider.CameraScale / _cameraConfig.MaxScaling * _cameraConfig.CameraScaleOnMovementInfluenceCoefficient;

    private BorderInfo _borderInfo;

    public CameraMovementService(ICameraProvider cameraProvider, IStaticDataService staticData, ITimeService time)
    {
      _cameraProvider = cameraProvider;
      _time = time;
      
      _cameraConfig = staticData.GetCameraConfig();
    }

    public void SetCameraBorders(LevelConfig config) =>
      _borderInfo = config.BorderInfo;

    public void MoveCamera(Vector2 moveDir)
    {
      Vector3 newCameraPos = _cameraProvider.CameraPosition + MoveVector(moveDir) * _time.DeltaTime;

      _cameraProvider.CameraPosition = CameraClamp(
        newCameraPos,
        _borderInfo.LeftDownBorder.position,
        _borderInfo.RightUpBorder.position,
        _cameraProvider.ScreenSizeInWorldPoints / 2);
    }

    public void RecalculateCameraPosition()
    {
      _cameraProvider.CameraPosition = CameraClamp(
        _cameraProvider.CameraPosition,
        _borderInfo.LeftDownBorder.position,
        _borderInfo.RightUpBorder.position,
        _cameraProvider.ScreenSizeInWorldPoints / 2);
    }

    private Vector3 MoveVector(Vector2 moveDir) =>
      moveDir.ToVector3() * _cameraConfig.MovementSpeed * CameraScaleInfluenceCoefficient;

    private Vector3 CameraClamp(Vector3 pos, Vector2 min, Vector2 max, Vector2 halfScreenSize) =>
      new(
        Mathf.Clamp(pos.x, min.x + halfScreenSize.x, max.x - halfScreenSize.x),
        Mathf.Clamp(pos.y, min.y + halfScreenSize.y, max.y - halfScreenSize.y), pos.z);
  }
}
