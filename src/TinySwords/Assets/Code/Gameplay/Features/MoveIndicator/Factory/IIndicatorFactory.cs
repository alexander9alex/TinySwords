using UnityEngine;

namespace Code.Gameplay.Features.MoveIndicator.Factory
{
  public interface IIndicatorFactory
  {
    GameEntity CreateMoveIndicator(Vector3 pos);
    GameEntity CreateAttackIndicator(Vector3 pos);
    GameEntity CreateIncorrectCommandIndicator(Vector3 pos);
  }
}
