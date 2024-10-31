using Code.Common.Entities;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Factory
{
  class UnitFactory : IUnitFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IIdentifierService _identifiers;

    public UnitFactory(IStaticDataService staticDataService, IIdentifierService identifiers)
    {
      _staticDataService = staticDataService;
      _identifiers = identifiers;
    }

    public void CreateUnit(UnitTypeId type, UnitColor color, Vector3 pos)
    {
      UnitConfig unitConfig = _staticDataService.GetUnitConfig(type, color);

      CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddViewPrefab(unitConfig.UnitPrefab)
        .AddWorldPosition(pos)
        ;
    }
  }
}
