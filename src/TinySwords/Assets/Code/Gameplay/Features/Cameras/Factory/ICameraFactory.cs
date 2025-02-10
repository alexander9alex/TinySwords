using Code.Gameplay.Level.Configs;

namespace Code.Gameplay.Features.Cameras.Factory
{
  public interface ICameraFactory
  {
    void CreateCamera(LevelConfig levelConfig);
  }
}
