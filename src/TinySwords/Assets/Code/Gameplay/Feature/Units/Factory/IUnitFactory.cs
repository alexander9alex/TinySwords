using Code.Gameplay.Feature.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Feature.Units.Factory
{
  public interface IUnitFactory
  {
    void CreateUnit(UnitTypeId type, UnitColor color, Vector3 pos);
  }
}
