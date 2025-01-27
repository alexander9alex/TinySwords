using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.FogOfWar.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Gameplay.Features.FogOfWar.Factory
{
  public class FogOfWarFactory : IFogOfWarFactory
  {
    private readonly IStaticDataService _staticData;

    public FogOfWarFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public void CreateFogOfWar(FogOfWarMarker fogOfWarMarker, Transform parent)
    {
      GameObject placeGO = fogOfWarMarker.gameObject;

      GameObject placeFogOfWar = Object.Instantiate(placeGO, placeGO.transform.position, placeGO.transform.rotation);
      placeFogOfWar.name += "_FogOfWar";
      placeFogOfWar.transform.parent = parent;

      TilemapRenderer placeFogOfWarTilemapRenderer = placeFogOfWar.GetComponent<TilemapRenderer>();
      placeFogOfWarTilemapRenderer.material = _staticData.GetFogOfWarConfig().Material;
      placeFogOfWarTilemapRenderer.sortingOrder = fogOfWarMarker.OrderInLayer;
    }
  }
}
