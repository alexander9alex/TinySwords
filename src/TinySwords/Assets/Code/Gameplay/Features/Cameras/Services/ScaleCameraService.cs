using System;
using System.Collections;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Cameras.Configs;
using Code.Infrastructure.Common.CoroutineRunner;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  class ScaleCameraService : IScaleCameraService
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly CameraConfig _cameraConfig;
    private readonly ICoroutineRunner _coroutineRunner;

    private float _endCameraScale;
    private bool _scalingNow;

    public ScaleCameraService(ICameraProvider cameraProvider, IStaticDataService staticData, ICoroutineRunner coroutineRunner)
    {
      _cameraProvider = cameraProvider;
      _coroutineRunner = coroutineRunner;
      _cameraConfig = staticData.GetCameraConfig();
    }

    public void ScaleCamera(float scale)
    {
      float newSize = _cameraProvider.CameraScale + scale * _cameraConfig.ScaleStep * _cameraProvider.CameraScale;
      _endCameraScale = Mathf.Clamp(newSize, _cameraConfig.MinScaling, _cameraConfig.MaxScaling);

      _coroutineRunner.StartCoroutine(ScaleCamera());
    }

    private IEnumerator ScaleCamera()
    {
      if (_scalingNow)
        yield break;

      _scalingNow = true;

      while (ScalingNotCompleted(_cameraProvider.CameraScale, _endCameraScale))
      {
        _cameraProvider.CameraScale = NewCameraScale(_cameraProvider.CameraScale, _endCameraScale);
        yield return null;
      }

      _scalingNow = false;
    }

    private float NewCameraScale(float cameraScale, float endScale) =>
      Mathf.Lerp(cameraScale, endScale, 1 - _cameraConfig.ScaleSmoothness);

    private bool ScalingNotCompleted(float cameraScale, float endScale) =>
      Math.Abs(cameraScale - endScale) > _cameraConfig.SmoothScaleTolerance;
  }
}
