using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Factory
{
  class UnitFactory : IUnitFactory
  {
    private readonly IStaticDataService _staticDataService;

    public UnitFactory(IStaticDataService staticDataService)
    {
      _staticDataService = staticDataService;
    }

    public void CreateUnit(UnitTypeId type, UnitColor color, Vector3 pos)
    {
      UnitConfig unitConfig = _staticDataService.GetUnitConfig(type, color);
    }
  }
}
