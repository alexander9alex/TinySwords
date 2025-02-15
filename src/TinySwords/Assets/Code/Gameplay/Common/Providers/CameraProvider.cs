using UnityEngine;

namespace Code.Gameplay.Common.Providers
{
  public class CameraProvider : ICameraProvider
  {
    private Camera _camera;
    public Vector3 CameraPosition
    {
      get => _camera.transform.position;
      set => _camera.transform.position = value;
    }
    public float CameraScale
    {
      get => _camera.orthographicSize;
      set
      {
        _camera.orthographicSize = value;
        CalculateScreenSize();
      }
    }

    public Vector2 ScreenSizeInWorldPoints => _screenSizeInWorldPoints;
    private Vector2 _screenSizeInWorldPoints;

    public void SetMainCamera(Camera camera)
    {
      _camera = camera;
      CalculateScreenSize();
    }

    private void CalculateScreenSize() =>
      _screenSizeInWorldPoints = (ViewportToWorldPoint(Vector3.one) - CameraPosition) * 2;

    public Vector3 ScreenToWorldPoint(Vector2 pos) =>
      _camera.ScreenToWorldPoint(pos);

    public Vector3 ViewportToWorldPoint(Vector2 pos) =>
      _camera.ViewportToWorldPoint(pos);
  }
}
