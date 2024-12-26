using System.Collections.Generic;
using Code.Gameplay.Constants;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Services
{
  class BattleFormationService : IBattleFormationService
  {
    public IEnumerable<Vector2> GetRectangleBattleFormation(Vector3 pos, int groupCount)
    {
      if (groupCount <= 1)
      {
        yield return pos;
        yield break;
      }

      int cols = Mathf.CeilToInt(Mathf.Sqrt(groupCount));
      int rows = Mathf.CeilToInt((float)groupCount / cols);

      Vector3 leftUpAngle = GetLeftUpAngle(pos, rows, cols);
      int placedUnits = 0;

      for (int y = 0; y < rows && placedUnits < groupCount; y++)
      for (int x = 0; x < cols && placedUnits < groupCount; x++)
      {
        yield return new Vector2(
          leftUpAngle.x + x * GameConstants.UnitMinRadius,
          leftUpAngle.y - y * GameConstants.UnitMinRadius
        );

        placedUnits++;
      }
    }

    private static Vector3 GetLeftUpAngle(Vector3 pos, int rows, int cols)
    {
      float offsetX = (cols - 1) / 2f * GameConstants.UnitMinRadius;
      float offsetY = (rows - 1) / 2f * GameConstants.UnitMinRadius;
      return pos + new Vector3(-offsetX, offsetY, 0);
    }
  }
}
