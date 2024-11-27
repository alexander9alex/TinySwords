using UnityEngine;

namespace Code.Gameplay.Features.Death.Factory
{
  public interface IUnitDeathFactory
  {
    void CreateDeathAnimation(Vector3 pos);
  }
}