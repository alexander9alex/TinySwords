using Code.Gameplay.Common.Providers;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  class ScaleCameraService : IScaleCameraService
  {
    private readonly ICameraProvider _cameraProvider;

    public ScaleCameraService(ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
    }

    public void ScaleCamera(float scale)
    {
      float newSize = _cameraProvider.MainCamera.orthographicSize + scale;
      _cameraProvider.MainCamera.orthographicSize = Mathf.Clamp(newSize, 1, 15);
    }
  }
}
