using Code.Gameplay.Feature.Units.Configs;
using Code.Gameplay.Feature.Units.Data;
using Code.Gameplay.Services;
using Code.Infrastructure.States.GameStates;
using UnityEngine;

namespace Code.Gameplay.Feature.Units.Factory
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
