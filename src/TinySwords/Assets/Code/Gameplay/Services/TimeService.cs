using UnityEngine;

namespace Code.Gameplay.Services
{
  class TimeService : ITimeService
  {
    public float DeltaTime => Time.deltaTime;
  }
}
