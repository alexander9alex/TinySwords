namespace Code.Gameplay.Services
{
  public interface ITimeService
  {
    float DeltaTime { get; }
    int TimeScale { get; set; }
    void FreezeTime();
    void UnfreezeTime();
  }
}
