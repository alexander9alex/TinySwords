using UnityEngine;

namespace Code.Gameplay.Common.Providers
{
  class CameraProvider : ICameraProvider
  {
    public Camera MainCamera { get; private set; }

    public void SetMainCamera(Camera camera) =>
      MainCamera = camera;
  }
}
