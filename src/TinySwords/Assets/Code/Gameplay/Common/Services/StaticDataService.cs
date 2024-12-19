using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Build.Configs;
using Code.Gameplay.Features.Command.Configs;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Death.Configs;
using Code.Gameplay.Features.Indicators.Configs;
using Code.Gameplay.Features.Indicators.Data;
using Code.Gameplay.Features.Sounds.Configs;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Common.Services
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<(UnitTypeId, TeamColor), UnitConfig> _unitConfigByTypeAndColor;
    private Dictionary<TeamColor, CastleConfig> _castleConfigByColor;
    private Dictionary<LevelId, LevelConfig> _levelConfigById;
    private Dictionary<CommandTypeId, CommandUIConfig> _commandUIConfigByType;
    private Dictionary<SoundId, SoundConfig> _soundConfigById;
    private Dictionary<IndicatorTypeId, IndicatorConfig> _indicatorConfigByType;

    public void LoadAll()
    {
      LoadUnitConfigs();
      LoadCastleConfigs();
      LoadLevelConfigs();
      LoadCommandUIConfigs();
      LoadSoundConfigs();
      LoadIndicatorConfigs();
    }

    public UnitConfig GetUnitConfig(UnitTypeId type, TeamColor color) =>
      _unitConfigByTypeAndColor[(type, color)];

    public EntityBehaviour GetHighlightViewPrefab() =>
      Resources.Load<EntityBehaviour>("UI/Highlight/Highlight");

    public IndicatorConfig GetIndicatorConfig(IndicatorTypeId typeId) =>
      _indicatorConfigByType[typeId];

    public CastleConfig GetCastleConfig(TeamColor color) =>
      _castleConfigByColor[color];

    public LevelConfig GetLevelConfig(LevelId levelId) =>
      _levelConfigById[levelId];

    public List<CommandUIConfig> GetUnitCommandUIConfigs(List<CommandTypeId> availableCommands)
    {
      return _commandUIConfigByType.Values
        .Where(config => availableCommands.Any(actionTypeId => config.CommandTypeId == actionTypeId))
        .ToList();
    }

    public CommandUIConfig GetCommandUIConfig(CommandTypeId commandTypeId) =>
      _commandUIConfigByType[commandTypeId];

    public UnitDeathConfig GetUnitDeathConfig() =>
      Resources.Load<UnitDeathConfig>("Configs/Units/UnitDeathConfig");

    public SoundConfig GetSoundConfig(SoundId soundId) =>
      _soundConfigById[soundId];

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
      _levelConfigById = Resources
        .LoadAll<LevelConfig>("Configs/Levels")
        .ToDictionary(x => x.LevelId, x => x);
    }

    private void LoadCommandUIConfigs()
    {
      _commandUIConfigByType = Resources
        .LoadAll<CommandUIConfig>("Configs/UI/Commands")
        .ToDictionary(x => x.CommandTypeId, x => x);
    }

    private void LoadSoundConfigs()
    {
      _soundConfigById = Resources
        .LoadAll<SoundConfig>("Configs/Sounds")
        .ToDictionary(x => x.SoundId, x => x);
    }

    private void LoadIndicatorConfigs()
    {
      _indicatorConfigByType = Resources
        .LoadAll<IndicatorConfig>("Configs/Indicators")
        .ToDictionary(x => x.IndicatorTypeId, x => x);
    }
  }
}
