using UnityEngine;

namespace Code.Gameplay.Features.Dead.Factory
{
  public interface IUnitDeathFactory
  {
    void CreateDeathAnimation(Vector3 pos);
  }
}