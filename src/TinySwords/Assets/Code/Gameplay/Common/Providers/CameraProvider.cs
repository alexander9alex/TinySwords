using UnityEngine;

namespace Code.Gameplay.Common.Providers
{
  public class CameraProvider : ICameraProvider
  {
    public Camera MainCamera { get; private set; }
    public float CameraScale
    {
      get => MainCamera.orthographicSize;
      set => MainCamera.orthographicSize = value;
    }

    public void SetMainCamera(Camera camera) =>
      MainCamera = camera;

    public Vector3 ScreenToWorldPoint(Vector2 pos) =>
      MainCamera.ScreenToWorldPoint(pos);
  }
}
