using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Build.Configs;
using Code.Gameplay.Features.Move.Configs;
using Code.Gameplay.Features.ParseAction.Configs;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Level.Configs;
using Code.Infrastructure.Views;
using Code.UI.Buttons.Configs;
using Code.UI.Buttons.Data;
using UnityEngine;

namespace Code.Gameplay.Common.Services
{
  class StaticDataService : IStaticDataService
  {
    private Dictionary<(UnitTypeId, TeamColor), UnitConfig> _unitConfigByTypeAndColor;
    private Dictionary<TeamColor, CastleConfig> _castleConfigByColor;
    private List<LevelConfig> _levelConfigs;
    private List<ControlButtonConfig> _controlButtonConfigs;
    private Dictionary<ActionTypeId, ActionConfig> _actionConfigByType;

    public void LoadAll()
    {
      LoadUnitConfigs();
      LoadCastleConfigs();
      LoadLevelConfigs();
      LoadControlButtonConfigs();
      LoadActionConfigs();
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

    public List<ControlButtonConfig> GetControlButtonConfigs(List<ActionTypeId> availableActions) =>
      _controlButtonConfigs
        .Where(config => availableActions
          .Any(actionTypeId => config.ActionTypeId == actionTypeId))
        .ToList();

    public GameObject GetActionDescription(ActionTypeId actionTypeId) =>
      _actionConfigByType[actionTypeId].DescriptionPrefab;

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

    private void LoadControlButtonConfigs()
    {
      _controlButtonConfigs = Resources
        .LoadAll<ControlButtonConfig>("Configs/UI/ControlButtons")
        .ToList();
    }

    private void LoadActionConfigs()
    {
      _actionConfigByType = Resources
        .LoadAll<ActionConfig>("Configs/UI/Actions")
        .ToDictionary(x => x.ActionTypeId, x => x);
    }
  }
}
