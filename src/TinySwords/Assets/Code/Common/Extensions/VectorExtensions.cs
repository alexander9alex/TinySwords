using UnityEngine;

namespace Code.Common.Extensions
{
  public static class VectorExtensions
  {
    public static Vector3 RemoveZ(this Vector3 v) =>
      new(v.x, v.y, 0);

    public static Vector3 SetZ(this Vector2 v, float z) =>
      new(v.x, v.y, z);
    
    public static Vector2 ToVector2(this Vector3 v) =>
      new(v.x, v.y);

    public static Vector3 ToVector3(this Vector2 v) =>
      new(v.x, v.y, 0);

    public static Vector4 ToVector4(this Vector2 v) =>
      new(v.x, v.y, 0, 0);

    public static Vector4 ReplaceZ(this Vector4 v, float z) =>
      new(v.x, v.y, z, v.w);
    
    public static Vector2 RemoveX(this Vector2 v) =>
      new(0, v.y);

    public static Vector2 RemoveY(this Vector2 v) =>
      new(v.x, 0);
    
    public static Vector2 Abs(this Vector2 v) =>
      new(Mathf.Abs(v.x), Mathf.Abs(v.y));
    public static Vector3 Abs(this Vector3 v) =>
      new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
  }
}