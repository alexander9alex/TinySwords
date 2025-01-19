using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar
{
  public class GlowingObject
  {
    public Vector2 Position { get; private set; }
    public float Radius { get; private set; }

    public GlowingObject(Vector2 position, float radius)
    {
      Position = position;
      Radius = radius;
    }
  }
}