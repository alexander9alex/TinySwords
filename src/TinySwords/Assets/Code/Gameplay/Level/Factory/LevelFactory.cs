using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
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
        .AddViewPrefab(config.MapPrefab)
        .AddWorldPosition(Vector3.zero)
        .With(x => x.isInitializationRequest = true)
        .With(x => x.isNotAddedNavMeshRootSource = true);
      
      CreateEntity.Empty()
        .With(x => x.isBuildNavMeshAtStart = true);

      foreach (CreateUnitMarker marker in CreateUnitMarkers(config))
        _unitFactory.CreateUnit(marker.UnitTypeId, marker.Color, marker.transform.position);
    }

    private static CreateUnitMarker[] CreateUnitMarkers(LevelConfig config) =>
      config.LevelMap.CreateMarkersParent.GetComponentsInChildren<CreateUnitMarker>();
  }
}
