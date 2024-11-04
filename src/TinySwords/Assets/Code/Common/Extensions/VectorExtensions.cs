using UnityEngine;

namespace Code.Common.Extensions
{
  public static class VectorExtensions
  {
    public static Vector3 RemoveZ(this Vector3 v3) =>
      new(v3.x, v3.y, 0);
  }
}