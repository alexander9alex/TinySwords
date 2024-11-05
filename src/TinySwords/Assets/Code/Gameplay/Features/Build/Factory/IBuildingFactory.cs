using Code.Gameplay.Features.Build.Data;
using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Build.Factory
{
  public interface IBuildingFactory
  {
    void CreateBuilding(BuildingTypeId typeId, TeamColor color, Vector3 pos);
  }
}
