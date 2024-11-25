using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Level.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Level Config", fileName = "LevelConfig", order = 0)]
  public class LevelConfig : ScriptableObject
  {
    public EntityBehaviour MapPrefab;
    public LevelMap LevelMap;
  }
}
