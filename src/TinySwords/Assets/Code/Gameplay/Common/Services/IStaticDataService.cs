using System.Collections.Generic;
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
  public interface IStaticDataService
  {
    void LoadAll();
    UnitConfig GetUnitConfig(UnitTypeId type, TeamColor color);
    EntityBehaviour GetHighlightViewPrefab();
    MoveClickIndicatorConfig GetMoveClickIndicatorConfig();
    CastleConfig GetCastleConfig(TeamColor color);
    LevelConfig GetLevelConfig();
    List<UnitActionUIConfig> GetUnitCommandUIConfigs(List<UnitCommandTypeId> availableCommands);
    UnitActionUIConfig GetUnitCommandUIConfig(UnitCommandTypeId unitCommandTypeId);
  }
}
