using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.FogOfWar.Data;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Features.Units.Markers;
using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Data;
using UnityEngine;

namespace Code.Gameplay.Level.Factory
{
  public class LevelFactory : ILevelFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly IUnitFactory _unitFactory;

    public LevelFactory(IStaticDataService staticData, IUnitFactory unitFactory)
    {
      _staticData = staticData;
      _unitFactory = unitFactory;
    }

    public void CreateLevel(LevelId levelId)
    {
      LevelConfig config = _staticData.GetLevelConfig(levelId);

      CreateEntity.Empty()
        .AddViewPrefab(config.LevelPrefab)
        .AddWorldPosition(Vector3.zero)
        .With(x => x.isInitializationRequest = true)
        .With(x => x.isNotAddedNavMeshRootSource = true)
        .With(x => x.isLevelParent = true);
      
      CreateEntity.Empty()
        .With(x => x.isBuildNavMeshAtStart = true);

      CreateUnits(config);
      CreateFogOfWar(config);
    }

    private void CreateUnits(LevelConfig config)
    {
      foreach (UnitMarker marker in UnitMarkers(config))
        _unitFactory.CreateUnit(marker.UnitTypeId, marker.Color, marker.transform.position);
    }

    private static void CreateFogOfWar(LevelConfig config)
    {
      foreach (FogOfWarMarker marker in config.LevelPrefab.GetComponentsInChildren<FogOfWarMarker>(includeInactive: false))
      {
        CreateEntity.Empty()
          .AddFogOfWarMarker(marker)
          .With(x => x.isCreateFogOfWar = true);
      }
    }

    private static UnitMarker[] UnitMarkers(LevelConfig config) =>
      config.LevelMarkersParent.GetComponentsInChildren<UnitMarker>(includeInactive: false);
  }
}
