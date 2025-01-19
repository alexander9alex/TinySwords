using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar.Services
{
  public class FogOfWarService : IFogOfWarService
  {
    // the value specified in the fog of war shader
    private const int MaxGlowingObjects = 1024;

    private static readonly int GlowingObjectsId = Shader.PropertyToID("_GlowingObjects");
    private static readonly int GlowingObjectCountId = Shader.PropertyToID("_GlowingObjectCount");

    private readonly List<GlowingObject> _glowingObjects = new();
    private readonly Material _fogOfWarMaterial;

    public FogOfWarService(IStaticDataService staticData) =>
      _fogOfWarMaterial = staticData.GetFogOfWarMaterial();

    public void UpdateFogOfWar()
    {
      _fogOfWarMaterial.SetVectorArray(GlowingObjectsId, PaddedGlowingObjects());
      _fogOfWarMaterial.SetInt(GlowingObjectCountId, _glowingObjects.Count);
    }

    public void AddGlowingObject(Vector2 position, float visionRadius) =>
      _glowingObjects.Add(new(position, visionRadius));

    public void ClearGlowingObjects() =>
      _glowingObjects.Clear();

    private List<Vector4> PaddedGlowingObjects()
    {
      List<Vector4> paddedGlowingObjects = GlowingObjects();

      while (paddedGlowingObjects.Count < MaxGlowingObjects)
        paddedGlowingObjects.Add(Vector4.zero);

      return paddedGlowingObjects;
    }

    private List<Vector4> GlowingObjects() =>
      new(_glowingObjects.Select(x => x.Position.ToVector4().ReplaceZ(x.Radius)).ToList());
  }
}
