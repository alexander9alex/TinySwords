using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Services
{
  public interface IStaticDataService
  {
    void LoadAll();
    UnitConfig GetUnitConfig(UnitTypeId type, UnitColor color);
    EntityBehaviour GetHighlightViewPrefab();
  }
}
