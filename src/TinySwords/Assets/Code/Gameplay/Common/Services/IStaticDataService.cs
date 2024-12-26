using System.Collections.Generic;
using Code.Gameplay.Features.Build.Configs;
using Code.Gameplay.Features.Cameras.Configs;
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

namespace Code.Gameplay.Common.Services
{
  public interface IStaticDataService
  {
    void LoadAll();
    UnitConfig GetUnitConfig(UnitTypeId type, TeamColor color);
    EntityBehaviour GetHighlightViewPrefab();
    IndicatorConfig GetIndicatorConfig(IndicatorTypeId typeId);
    CastleConfig GetCastleConfig(TeamColor color);
    LevelConfig GetLevelConfig(LevelId levelId);
    List<CommandUIConfig> GetUnitCommandUIConfigs(List<CommandTypeId> availableCommands);
    CommandUIConfig GetCommandUIConfig(CommandTypeId commandTypeId);
    UnitDeathConfig GetUnitDeathConfig();
    SoundConfig GetSoundConfig(SoundId soundId);
    CameraConfig GetCameraConfig();
  }
}
