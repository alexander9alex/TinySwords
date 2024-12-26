using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Cameras.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  class ScaleCameraService : IScaleCameraService
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly CameraConfig _cameraConfig;

    public ScaleCameraService(ICameraProvider cameraProvider, IStaticDataService staticData)
    {
      _cameraProvider = cameraProvider;
      _cameraConfig = staticData.GetCameraConfig();
    }

    public void ScaleCamera(float scale)
    {
      float newSize = _cameraProvider.MainCamera.orthographicSize + scale * _cameraConfig.ScaleSpeed;
      _cameraProvider.MainCamera.orthographicSize = Mathf.Clamp(newSize, _cameraConfig.MinScaling, _cameraConfig.MaxScaling);
    }
  }
}
