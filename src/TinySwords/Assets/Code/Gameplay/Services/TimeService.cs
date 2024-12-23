using UnityEngine;

namespace Code.Gameplay.Services
{
  public class TimeService : ITimeService
  {
    public float DeltaTime => Time.deltaTime * TimeScale;
    public int TimeScale { get; set; } = 1;
  }
}
