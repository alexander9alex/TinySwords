using Code.Gameplay.Features.Move.Configs;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;

namespace Code.Gameplay.Common.Services
{
  public interface IStaticDataService
  {
    void LoadAll();
    UnitConfig GetUnitConfig(UnitTypeId type, UnitColor color);
    EntityBehaviour GetHighlightViewPrefab();
    MoveClickIndicatorConfig GetMoveClickIndicatorConfig();
  }
}
