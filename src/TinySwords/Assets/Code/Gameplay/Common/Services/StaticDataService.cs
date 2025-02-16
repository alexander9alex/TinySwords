using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.CutScene.Configs;
using Code.Gameplay.CutScene.Data;
using Code.Gameplay.Features.Cameras.Configs;
using Code.Gameplay.Features.Command.Configs;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Death.Configs;
using Code.Gameplay.Features.FogOfWar.Configs;
using Code.Gameplay.Features.Indicators.Configs;
using Code.Gameplay.Features.Indicators.Data;
using Code.Gameplay.Features.Sounds.Configs;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Data;
using Code.Infrastructure.Scenes.Configs;
using Code.Infrastructure.Scenes.Data;
using Code.Infrastructure.Views;
using Code.UI.Data;
using Code.UI.Windows.Data;
using UnityEngine;

namespace Code.Gameplay.Common.Services
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<(UnitTypeId, TeamColor), UnitConfig> _unitConfigByTypeAndColor;
    private Dictionary<LevelId, LevelConfig> _levelConfigById;
    private Dictionary<CommandTypeId, CommandUIConfig> _commandUIConfigByType;
    private Dictionary<SoundId, SoundConfig> _soundConfigById;
    private Dictionary<IndicatorTypeId, IndicatorConfig> _indicatorConfigByType;
    private Dictionary<SceneId, SceneConfig> _sceneConfigById;
    private Dictionary<CutSceneId, CutSceneConfig> _cutSceneConfigById;
    private Dictionary<WindowId, WindowConfig> _windowConfigById;

    public void LoadAll()
    {
      LoadUnitConfigs();
      LoadLevelConfigs();
      LoadCommandUIConfigs();
      LoadSoundConfigs();
      LoadIndicatorConfigs();
      LoadSceneConfigs();
      LoadCutSceneConfigs();
      LoadWindowConfigs();
    }

    public UnitConfig GetUnitConfig(UnitTypeId type, TeamColor color) =>
      _unitConfigByTypeAndColor[(type, color)];

    public EntityBehaviour GetHighlightPrefab() =>
      Resources.Load<EntityBehaviour>("UI/Highlight/Highlight");

    public IndicatorConfig GetIndicatorConfig(IndicatorTypeId typeId) =>
      _indicatorConfigByType[typeId];

    public LevelConfig GetLevelConfig(LevelId levelId) =>
      _levelConfigById[levelId];

    public List<CommandUIConfig> GetUnitCommandUIConfigs(List<CommandTypeId> availableCommands)
    {
      return _commandUIConfigByType
        .Values
        .Where(config => availableCommands.Any(actionTypeId => config.CommandTypeId == actionTypeId))
        .ToList();
    }

    public CommandUIConfig GetCommandUIConfig(CommandTypeId commandTypeId) =>
      _commandUIConfigByType[commandTypeId];

    public UnitDeathConfig GetUnitDeathConfig() =>
      Resources.Load<UnitDeathConfig>("Configs/Death/UnitDeathConfig");

    public SoundConfig GetSoundConfig(SoundId soundId) =>
      _soundConfigById[soundId];

    public CameraConfig GetCameraConfig() =>
      Resources.Load<CameraConfig>("Configs/Camera/CameraConfig");

    public string GetSceneNameById(SceneId sceneId) =>
      _sceneConfigById[sceneId].SceneName;

    public CutSceneConfig GetCutSceneConfig(CutSceneId cutSceneId) =>
      _cutSceneConfigById[cutSceneId];

    public FogOfWarConfig GetFogOfWarConfig() =>
      Resources.Load<FogOfWarConfig>("Configs/Shaders/FogOfWarConfig");

    public GameObject GetWindowPrefab(WindowId windowId) =>
      _windowConfigById[windowId].WindowPrefab;

    private void LoadUnitConfigs()
    {
      _unitConfigByTypeAndColor = Resources
        .LoadAll<UnitConfig>("Configs/Units")
        .ToDictionary(x => (x.TypeId, x.Color), x => x);
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

    private void LoadSceneConfigs()
    {
      _sceneConfigById = Resources
        .LoadAll<SceneConfig>("Configs/Scenes")
        .ToDictionary(x => x.SceneId, x => x);
    }

    private void LoadCutSceneConfigs()
    {
      _cutSceneConfigById = Resources
        .LoadAll<CutSceneConfig>("Configs/CutScenes")
        .ToDictionary(x => x.CutSceneId, x => x);
    }

    private void LoadWindowConfigs()
    {
      _windowConfigById = Resources
        .LoadAll<WindowConfig>("Configs/UI/Windows")
        .ToDictionary(x => x.WindowId, x => x);
    }
  }
}
