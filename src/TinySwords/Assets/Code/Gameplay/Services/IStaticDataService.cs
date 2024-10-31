using Code.Gameplay.Feature.Units.Configs;
using Code.Gameplay.Feature.Units.Data;
using Code.Infrastructure.States.GameStates;

namespace Code.Gameplay.Services
{
  public interface IStaticDataService
  {
    UnitConfig GetUnitConfig(UnitTypeId type, UnitColor color);
    void LoadAll();
  }
}
