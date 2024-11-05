using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Build.Configs;
using Code.Gameplay.Features.Move.Configs;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Common.Services
{
  class StaticDataService : IStaticDataService
  {
    private Dictionary<(UnitTypeId, TeamColor), UnitConfig> _unitConfigByTypeAndColor;
    private Dictionary<TeamColor, CastleConfig> _castleConfigByColor;

    public void LoadAll()
    {
      LoadUnitConfigs();
      LoadCastleConfigs();
    }

    public UnitConfig GetUnitConfig(UnitTypeId type, TeamColor color) =>
      _unitConfigByTypeAndColor[(type, color)];

    public EntityBehaviour GetHighlightViewPrefab() =>
      Resources.Load<EntityBehaviour>("UI/Highlight/Highlight");

    public MoveClickIndicatorConfig GetMoveClickIndicatorConfig() =>
      Resources.Load<MoveClickIndicatorConfig>("Configs/MoveClickIndicator/MoveClickIndicatorConfig");

    public CastleConfig GetCastleConfig(TeamColor color) =>
      _castleConfigByColor[color];

    private void LoadUnitConfigs()
    {
      _unitConfigByTypeAndColor = Resources
        .LoadAll<UnitConfig>("Configs/Units")
        .ToDictionary(x => (x.TypeId, x.Color), x => x);
    }

    private void LoadCastleConfigs()
    {
      _castleConfigByColor = Resources
        .LoadAll<CastleConfig>("Configs/Castles")
        .ToDictionary(x => x.Color, x => x);
    }
  }
}
