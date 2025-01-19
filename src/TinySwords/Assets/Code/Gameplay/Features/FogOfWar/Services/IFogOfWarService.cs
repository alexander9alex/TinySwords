using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar.Services
{
  public interface IFogOfWarService
  {
    void InitializeFogOfWar();
    void ClearGlowingObjects();
    void UpdateFogOfWar();
    void AddGlowingObject(Vector2 position, float visionRadius);
    bool IsPositionVisible(Vector2 pos);
  }
}
