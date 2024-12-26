using Code.Infrastructure.Scenes.Data;
using UnityEngine;

namespace Code.Infrastructure.Scenes.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Scene Config", fileName = "SceneConfig", order = 0)]
  public class SceneConfig : ScriptableObject
  {
    public SceneId SceneId;
    public string SceneName;
  }
}