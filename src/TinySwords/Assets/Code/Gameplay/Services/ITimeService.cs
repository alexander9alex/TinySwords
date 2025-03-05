namespace Code.Gameplay.Services
{
  public interface ITimeService
  {
    float DeltaTime { get; }
    int TimeScale { get; set; }
    void StopTime();
    void StartTime();
  }
}
