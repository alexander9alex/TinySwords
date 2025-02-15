using System;
using System.Collections;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Cameras.Configs;
using Code.Infrastructure.Common.CoroutineRunner;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Services
{
  class CameraScalingService : ICameraScalingService
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly CameraConfig _cameraConfig;
    private readonly ICoroutineRunner _coroutineRunner;

    private float _endCameraScale;
    private bool _scalingNow;
    private readonly ICameraMovementService _cameraMovementService;

    public CameraScalingService(ICameraProvider cameraProvider, IStaticDataService staticData, ICoroutineRunner coroutineRunner,
      ICameraMovementService cameraMovementService)
    {
      _cameraProvider = cameraProvider;
      _coroutineRunner = coroutineRunner;
      _cameraMovementService = cameraMovementService;
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
        ChangeCameraScale(NewCameraScale(_cameraProvider.CameraScale, _endCameraScale));
        yield return null;
      }

      ChangeCameraScale(_endCameraScale);

      _scalingNow = false;
    }

    private void ChangeCameraScale(float scale)
    {
      _cameraProvider.CameraScale = scale;
      _cameraMovementService.RecalculateCameraPosition();
    }

    private float NewCameraScale(float cameraScale, float endScale) =>
      Mathf.Lerp(cameraScale, endScale, 1 - _cameraConfig.ScaleSmoothness);

    private bool ScalingNotCompleted(float cameraScale, float endScale) =>
      Math.Abs(cameraScale - endScale) > _cameraConfig.SmoothScaleTolerance;
  }
}
