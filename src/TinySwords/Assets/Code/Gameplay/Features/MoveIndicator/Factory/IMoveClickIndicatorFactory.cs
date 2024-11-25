using UnityEngine;

namespace Code.Gameplay.Features.MoveIndicator.Factory
{
  public interface IMoveClickIndicatorFactory
  {
    GameEntity CreateMoveIndicator(Vector3 pos);
  }
}
