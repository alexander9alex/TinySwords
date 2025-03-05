using UnityEngine;

namespace Code.Gameplay.Services
{
  public class TimeService : ITimeService
  {
    public float DeltaTime => Time.deltaTime * TimeScale;
    public int TimeScale
    {
      get => _isTimeFreeze ? 0 : _timeScale;
      set => _timeScale = value;
    }
    private int _timeScale = 1;

    private bool _isTimeFreeze;
    
    public void FreezeTime() =>
      _isTimeFreeze = true;

    public void UnfreezeTime() =>
      _isTimeFreeze = false;
  }
}
