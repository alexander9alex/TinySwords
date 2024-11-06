using UnityEngine;

namespace Code.Common.Extensions
{
  public static class VectorExtensions
  {
    public static Vector3 RemoveZ(this Vector3 v) =>
      new(v.x, v.y, 0);

    public static Vector3 SetZ(this Vector2 v, float z) =>
      new(v.x, v.y, z);
  }
}