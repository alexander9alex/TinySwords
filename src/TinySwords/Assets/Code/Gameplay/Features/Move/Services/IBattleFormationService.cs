using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Services
{
  public interface IBattleFormationService
  {
    IEnumerable<Vector2> GetRectangleBattleFormation(Vector3 pos, int groupCount);
  }
}
