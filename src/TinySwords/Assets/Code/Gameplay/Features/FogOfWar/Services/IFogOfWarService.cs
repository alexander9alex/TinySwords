using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar.Services
{
  public interface IFogOfWarService
  {
    void ClearGlowingObjects();
    void UpdateFogOfWar();
    void UpdateGlowingObjectPosition(Vector2 position);
  }
}
