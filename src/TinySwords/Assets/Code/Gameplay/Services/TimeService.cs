using UnityEngine;

namespace Code.Gameplay.Services
{
  public class TimeService : ITimeService
  {
    public float DeltaTime => Time.deltaTime;
  }
}
