using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Data;

namespace Code.Gameplay.Level.Factory
{
  public interface ILevelFactory
  {
    void CreateLevel(LevelConfig config);
  }
}
