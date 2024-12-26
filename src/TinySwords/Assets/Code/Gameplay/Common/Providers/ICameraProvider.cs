using UnityEngine;

namespace Code.Gameplay.Common.Providers
{
  public interface ICameraProvider
  {
    Camera MainCamera { get; }
    float CameraScale { get; set; }
    void SetMainCamera(Camera camera);
  }
}
