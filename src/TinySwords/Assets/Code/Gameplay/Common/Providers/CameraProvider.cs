using UnityEngine;

namespace Code.Gameplay.Common.Providers
{
  public class CameraProvider : ICameraProvider
  {
    public Camera MainCamera { get; private set; }

    public void SetMainCamera(Camera camera) =>
      MainCamera = camera;
  }
}
