using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar
{
  public class GlowingObject
  {
    public Vector2 Position { get; private set; }
    public float VisionRadius { get; private set; }

    public GlowingObject(Vector2 position, float visionRadius)
    {
      Position = position;
      VisionRadius = visionRadius;
    }
  }
}