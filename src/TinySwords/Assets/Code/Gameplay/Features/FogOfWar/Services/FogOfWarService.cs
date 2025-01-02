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
    
    private static readonly int GlowingObjectPositionsId = Shader.PropertyToID("_GlowingObjectPositions");
    private static readonly int GlowingObjectCountId = Shader.PropertyToID("_GlowingObjectCount");

    private readonly List<Vector2> _glowingObjectPositions = new();
    private readonly Material _fogOfWarMaterial;

    public FogOfWarService(IStaticDataService staticData) =>
      _fogOfWarMaterial = staticData.GetFogOfWarMaterial();

    public void UpdateFogOfWar()
    {
      _fogOfWarMaterial.SetVectorArray(GlowingObjectPositionsId, GlowingObjectPositions());
      _fogOfWarMaterial.SetInt(GlowingObjectCountId, _glowingObjectPositions.Count);
    }

    public void UpdateGlowingObjectPosition(Vector2 position) =>
      _glowingObjectPositions.Add(position);

    public void ClearGlowingObjects() =>
      _glowingObjectPositions.Clear();

    private List<Vector4> GlowingObjectPositions()
    {
      List<Vector4> paddedPositions = new(_glowingObjectPositions.Select(x => x.ToVector4()).ToList());

      while (paddedPositions.Count < MaxGlowingObjects)
        paddedPositions.Add(Vector4.zero);

      return paddedPositions;
    }
  }
}
