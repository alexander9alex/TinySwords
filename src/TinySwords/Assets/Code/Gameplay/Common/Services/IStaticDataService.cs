using Code.Gameplay.Features.Build.Configs;
using Code.Gameplay.Features.Move.Configs;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;

namespace Code.Gameplay.Common.Services
{
  public interface IStaticDataService
  {
    void LoadAll();
    UnitConfig GetUnitConfig(UnitTypeId type, TeamColor color);
    EntityBehaviour GetHighlightViewPrefab();
    MoveClickIndicatorConfig GetMoveClickIndicatorConfig();
    CastleConfig GetCastleConfig(TeamColor color);
  }
}
