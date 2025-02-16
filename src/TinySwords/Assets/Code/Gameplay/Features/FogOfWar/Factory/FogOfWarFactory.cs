using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.FogOfWar.Configs;
using Code.Gameplay.Features.FogOfWar.Data;
using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar.Factory
{
  public class FogOfWarFactory : IFogOfWarFactory
  {
    private readonly FogOfWarConfig _fogOfWarConfig;

    public FogOfWarFactory(IStaticDataService staticData) =>
      _fogOfWarConfig = staticData.GetFogOfWarConfig();

    public void CreateFogOfWar(FogOfWarMarker fogOfWarMarker, Transform parent)
    {
      GameObject prefab = fogOfWarMarker.gameObject;
      GameObject fogOfWar = Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);

      Object.Destroy(fogOfWar.GetComponent<FogOfWarMarker>());
      fogOfWar.name += "_FogOfWar";
      fogOfWar.transform.parent = parent;

      foreach (Renderer renderer in fogOfWar.GetComponentsInChildren<Renderer>())
        SetFogOfWarShader(renderer, fogOfWarMarker);
    }

    private void SetFogOfWarShader(Renderer renderer, FogOfWarMarker fogOfWarMarker)
    {
      renderer.material = _fogOfWarConfig.Material;
      renderer.sortingOrder = fogOfWarMarker.OrderInLayer;
      UpdateSpriteRenderer(renderer.gameObject);
    }

    // Without this, there is a problem with correctly displaying the textures of the fog of war
    private static void UpdateSpriteRenderer(GameObject gameObject)
    {
      if (!gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
        return;

      Sprite sprite = spriteRenderer.sprite;
      spriteRenderer.sprite = null;
      spriteRenderer.sprite = sprite;
    }
  }
}
