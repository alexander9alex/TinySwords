using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;

namespace Code.Gameplay.Services
{
  public interface IStaticDataService
  {
    void LoadAll();
    UnitConfig GetUnitConfig(UnitTypeId type, UnitColor color);
    EntityBehaviour GetHighlightViewPrefab();
    EntityBehaviour GetMoveIndicatorPrefab();
  }
}
