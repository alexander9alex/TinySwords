using Code.Gameplay.Level.Data;
using Code.Infrastructure.Views;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Level.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Level Config", fileName = "LevelConfig", order = 0)]
  public class LevelConfig : ScriptableObject
  {
    public LevelId LevelId;

    [Header("Fill in for collect level setup")]
    public EntityBehaviour MapPrefab;
    
    [Header("Level Setup")]
    public LevelMap LevelMap;
    public BorderInfo BorderInfo;
  }
}
