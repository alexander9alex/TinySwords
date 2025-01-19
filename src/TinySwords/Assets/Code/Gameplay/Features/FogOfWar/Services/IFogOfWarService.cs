using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar.Services
{
  public interface IFogOfWarService
  {
    void ClearGlowingObjects();
    void UpdateFogOfWar();
    void AddGlowingObject(Vector2 position, float visionRadius);
  }
}
