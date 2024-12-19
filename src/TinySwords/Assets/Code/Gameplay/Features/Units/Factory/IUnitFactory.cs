using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Factory
{
  public interface IUnitFactory
  {
    GameEntity CreateUnit(UnitTypeId type, TeamColor color, Vector3 pos);
  }
}
