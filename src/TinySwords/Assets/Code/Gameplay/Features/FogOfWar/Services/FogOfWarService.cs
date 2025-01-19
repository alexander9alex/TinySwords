using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.FogOfWar.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.FogOfWar.Services
{
  public class FogOfWarService : IFogOfWarService
  {
    // the value specified in the fog of war shader
    private const int MaxGlowingObjects = 1024;

    private static readonly int GlowingObjectsId = Shader.PropertyToID("_GlowingObjects");
    private static readonly int GlowingObjectCountId = Shader.PropertyToID("_GlowingObjectCount");
    private static readonly int FogIntensityId = Shader.PropertyToID("_FogIntensity");
    private static readonly int VisibilitySmoothnessId = Shader.PropertyToID("_VisibilitySmoothness");

    private readonly List<GlowingObject> _glowingObjects = new();
    private readonly FogOfWarConfig _fogOfWarConfig;

    public FogOfWarService(IStaticDataService staticData) =>
      _fogOfWarConfig = staticData.GetFogOfWarConfig();

    public void InitializeFogOfWar()
    {
      _fogOfWarConfig.Material.SetFloat(FogIntensityId, _fogOfWarConfig.Intensity);
      _fogOfWarConfig.Material.SetFloat(VisibilitySmoothnessId, _fogOfWarConfig.VisibilitySmoothness);
    }

    public void UpdateFogOfWar()
    {
      _fogOfWarConfig.Material.SetVectorArray(GlowingObjectsId, PaddedGlowingObjects());
      _fogOfWarConfig.Material.SetInt(GlowingObjectCountId, _glowingObjects.Count);
    }

    public void AddGlowingObject(Vector2 position, float visionRadius) =>
      _glowingObjects.Add(new(position, visionRadius));

    public bool IsPositionVisible(Vector2 pos)
    {
      foreach (GlowingObject glowingObject in _glowingObjects)
      {
        if (Vector2.Distance(glowingObject.Position, pos) <= glowingObject.VisionRadius + FogOfWarSmoothnessCoefficient())
          return true;
      }

      return false;
    }

    private float FogOfWarSmoothnessCoefficient() =>
      _fogOfWarConfig.VisibilitySmoothness * 1;

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
      new(_glowingObjects.Select(x => x.Position.ToVector4().ReplaceZ(x.VisionRadius)).ToList());
  }
}
