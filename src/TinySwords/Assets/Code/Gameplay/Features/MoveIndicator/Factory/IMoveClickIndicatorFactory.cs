using UnityEngine;

namespace Code.Gameplay.Features.Move.Factory
{
  public interface IMoveClickIndicatorFactory
  {
    GameEntity CreateMoveIndicator(Vector3 pos);
  }
}
