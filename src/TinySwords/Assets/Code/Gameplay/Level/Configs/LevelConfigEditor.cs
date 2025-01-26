using Code.Gameplay.Level.Data;
using UnityEditor;
using UnityEngine;

namespace Code.Gameplay.Level.Configs
{
  [CustomEditor(typeof(LevelConfig))]
  public class LevelConfigEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelConfig levelConfig = (LevelConfig)target;

      if (GUILayout.Button("Collect"))
      {
        levelConfig.LevelMap = levelConfig.MapPrefab.GetComponent<LevelMap>();
        levelConfig.BorderInfo = levelConfig.MapPrefab.GetComponent<BorderInfo>();
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}
