using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Build.Configs;
using Code.Gameplay.Features.ControlAction.Configs;
using Code.Gameplay.Features.ControlAction.Data;
using Code.Gameplay.Features.Move.Configs;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Level.Configs;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Common.Services
{
  class StaticDataService : IStaticDataService
  {
    private Dictionary<(UnitTypeId, TeamColor), UnitConfig> _unitConfigByTypeAndColor;
    private Dictionary<TeamColor, CastleConfig> _castleConfigByColor;
    private List<LevelConfig> _levelConfigs;
    private Dictionary<UnitCommandTypeId, UnitActionUIConfig> _unitCommandUIConfigByType;

    public void LoadAll()
    {
      LoadUnitConfigs();
      LoadCastleConfigs();
      LoadLevelConfigs();
      LoadCommandUIConfigs();
    }

    public UnitConfig GetUnitConfig(UnitTypeId type, TeamColor color) =>
      _unitConfigByTypeAndColor[(type, color)];

    public EntityBehaviour GetHighlightViewPrefab() =>
      Resources.Load<EntityBehaviour>("UI/Highlight/Highlight");

    public MoveClickIndicatorConfig GetMoveClickIndicatorConfig() =>
      Resources.Load<MoveClickIndicatorConfig>("Configs/MoveClickIndicator/MoveClickIndicatorConfig");

    public CastleConfig GetCastleConfig(TeamColor color) =>
      _castleConfigByColor[color];

    public LevelConfig GetLevelConfig() =>
      _levelConfigs[0];

    public List<UnitActionUIConfig> GetUnitCommandUIConfigs(List<UnitCommandTypeId> availableCommands)
    {
      return _unitCommandUIConfigByType.Values
        .Where(config => availableCommands.Any(actionTypeId => config.UnitCommandTypeId == actionTypeId))
        .ToList();
    }

    public UnitActionUIConfig GetUnitCommandUIConfig(UnitCommandTypeId unitCommandTypeId) =>
      _unitCommandUIConfigByType[unitCommandTypeId];

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

    private void LoadLevelConfigs()
    {
      _levelConfigs = Resources
        .LoadAll<LevelConfig>("Configs/Levels")
        .ToList();
    }

    private void LoadCommandUIConfigs()
    {
      _unitCommandUIConfigByType = Resources
        .LoadAll<UnitActionUIConfig>("Configs/UI/Actions/Units")
        .ToDictionary(x => x.UnitCommandTypeId, x => x);
    }
  }
}
