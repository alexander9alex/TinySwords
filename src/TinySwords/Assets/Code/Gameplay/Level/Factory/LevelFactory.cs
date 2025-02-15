using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.FogOfWar.Data;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Features.Units.Markers;
using Code.Gameplay.Level.Configs;
using UnityEngine;

namespace Code.Gameplay.Level.Factory
{
  public class LevelFactory : ILevelFactory
  {
    private readonly IUnitFactory _unitFactory;

    public LevelFactory(IUnitFactory unitFactory) =>
      _unitFactory = unitFactory;

    public void CreateLevel(LevelConfig config)
    {
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
      CreateWinCondition();
    }

    private void CreateWinCondition()
    {
      CreateEntity.Empty()
        .isCreateWinCondition = true;
    }

    private void CreateUnits(LevelConfig config)
    {
      foreach (UnitMarker marker in UnitMarkers(config))
        _unitFactory.CreateUnit(marker);
    }

    private static void CreateFogOfWar(LevelConfig config)
    {
      foreach (FogOfWarMarker marker in config.LevelPrefab.GetComponentsInChildren<FogOfWarMarker>(includeInactive: false))
        CreateFogOfWar(marker);
    }

    private static void CreateFogOfWar(FogOfWarMarker marker)
    {
      CreateEntity.Empty()
        .AddFogOfWarMarker(marker)
        .With(x => x.isCreateFogOfWar = true);
    }

    private static UnitMarker[] UnitMarkers(LevelConfig config) =>
      config.LevelMarkersParent.GetComponentsInChildren<UnitMarker>(includeInactive: false);
  }
}
