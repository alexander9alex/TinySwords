using Code.Gameplay.Features.FogOfWar.Data;
using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar.Factory
{
  public interface IFogOfWarFactory
  {
    void CreateFogOfWar(FogOfWarMarker fogOfWarMarker, Transform parent);
  }
}
