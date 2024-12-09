using System.Collections.Generic;
using Code.Gameplay.Constants;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Services
{
  class BattleFormationService : IBattleFormationService
  {
    public IEnumerable<Vector2> GetSquareBattleFormation(Vector3 pos, int groupCount)
    {
      if (groupCount <= 1)
      {
        yield return pos;
        yield break;
      }

      float groupCountSquareRoot = Mathf.Ceil(Mathf.Sqrt(groupCount));
      Vector3 leftUpAngle = GetLeftUpAngle(pos, groupCountSquareRoot);

      for (int y = 0; y < groupCountSquareRoot; y++)
      for (int x = 0; x < groupCountSquareRoot; x++)
      {
        yield return leftUpAngle + new Vector3(x, -y) * GameConstants.UnitMinRadius;
      }
    }

    private static Vector3 GetLeftUpAngle(Vector3 pos, float countSquareRoot)
    {
      float offsetCoef = countSquareRoot / 2;
      return pos + new Vector3(-offsetCoef, offsetCoef) * GameConstants.UnitMinRadius + new Vector3(0.5f, -0.5f) * GameConstants.UnitMinRadius;
    }
  }
}
