using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Data
{
  public class CameraSpawnMarker : MonoBehaviour
  {
    public float Size;

    private void OnDrawGizmosSelected() =>
      DrawRectangle(transform.position, Size);

    private void DrawRectangle(Vector2 pos, float orthographicSize)
    {
      Vector2 halfCameraSize = new(orthographicSize * Screen.width / Screen.height, orthographicSize);
      
      Gizmos.color = new Color(0.04f, 0.49f, 1f);
      
      Gizmos.DrawLine(pos - halfCameraSize, pos + new Vector2(halfCameraSize.x, -halfCameraSize.y));
      Gizmos.DrawLine(pos - halfCameraSize, pos + new Vector2(-halfCameraSize.x, halfCameraSize.y));
      Gizmos.DrawLine(pos + halfCameraSize, pos + new Vector2(halfCameraSize.x, -halfCameraSize.y));
      Gizmos.DrawLine(pos + halfCameraSize, pos + new Vector2(-halfCameraSize.x, halfCameraSize.y));
      
      Gizmos.color = default;
    }
  }
}
