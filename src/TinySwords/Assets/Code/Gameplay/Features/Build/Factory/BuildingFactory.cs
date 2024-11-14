using System;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Build.Configs;
using Code.Gameplay.Features.Build.Data;
using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Build.Factory
{
  public class BuildingFactory : IBuildingFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly IIdentifierService _identifiers;

    public BuildingFactory(IStaticDataService staticData, IIdentifierService identifiers)
    {
      _staticData = staticData;
      _identifiers = identifiers;
    }

    public void CreateBuilding(BuildingTypeId typeId, TeamColor color, Vector3 pos)
    {
      switch (typeId)
      {
        case BuildingTypeId.Castle:
          CreateCastle(color, pos);
          break;
        case BuildingTypeId.House:
          break;
        case BuildingTypeId.Tower:
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(typeId), typeId, null);
      }
    }

    private void CreateCastle(TeamColor color, Vector3 pos)
    {
      CastleConfig config = _staticData.GetCastleConfig(color);
      CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddViewPrefab(config.CastlePrefab)
        .AddWorldPosition(pos)
        .AddBuildTypeId(BuildingTypeId.Castle)
        .With(x => x.isBuilding = true)
        .With(x => x.isCastle = true)
        .With(x => x.isSelectable = true)
        .With(x => x.isUnselected = true)
        .With(x => x.isUpdatePositionAfterSpawning = true)
        .With(x => x.isNotAddedNavMeshRootSource = true)
        .With(x => x.isAddToNavMeshCachedSources = true)
        ;
    }
  }
}
