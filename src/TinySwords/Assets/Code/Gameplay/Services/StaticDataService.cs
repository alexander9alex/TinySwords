using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Services
{
  class StaticDataService : IStaticDataService
  {
    private Dictionary<(UnitTypeId, UnitColor), UnitConfig> _unitConfigByTypeAndColor;

    public void LoadAll()
    {
      LoadUnitConfigs();
    }

    public UnitConfig GetUnitConfig(UnitTypeId type, UnitColor color) =>
      _unitConfigByTypeAndColor[(type, color)];

    private void LoadUnitConfigs()
    {
      _unitConfigByTypeAndColor = Resources
        .LoadAll<UnitConfig>("Configs/Units")
        .ToDictionary(x => (x.TypeId, x.Color), x => x);
    }
  }
}
