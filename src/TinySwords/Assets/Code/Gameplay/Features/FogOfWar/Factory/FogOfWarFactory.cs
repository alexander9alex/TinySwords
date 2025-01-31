using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.FogOfWar.Data;
using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar.Factory
{
  public class FogOfWarFactory : IFogOfWarFactory
  {
    private readonly IStaticDataService _staticData;

    public FogOfWarFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public void CreateFogOfWar(FogOfWarMarker fogOfWarMarker, Transform parent)
    {
      GameObject objectPrefab = fogOfWarMarker.gameObject;

      GameObject fogOfWar = Object.Instantiate(objectPrefab, objectPrefab.transform.position, objectPrefab.transform.rotation);
      fogOfWar.name += "_FogOfWar";
      fogOfWar.transform.parent = parent;

      foreach (Renderer renderer in fogOfWar.GetComponentsInChildren<Renderer>())
      {
        renderer.material = _staticData.GetFogOfWarConfig().Material;
        renderer.sortingOrder = fogOfWarMarker.OrderInLayer;
      }
    }
  }
}
