using UnityEngine;

namespace Code.Gameplay.Common.Providers
{
  public interface ICameraProvider
  {
    Vector3 CameraPosition { get; set; }
    float CameraScale { get; set; }
    Vector2 ScreenSizeInWorldPoints { get; }
    void SetMainCamera(Camera camera);
    Vector3 ScreenToWorldPoint(Vector2 pos);
    Vector3 ViewportToWorldPoint(Vector2 pos);
  }
}
