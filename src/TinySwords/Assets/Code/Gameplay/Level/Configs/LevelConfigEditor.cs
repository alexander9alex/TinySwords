using System.Linq;
using Code.Gameplay.Features.Cameras.Data;
using Code.Gameplay.Features.FogOfWar.Data;
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
        levelConfig.BorderInfo = levelConfig.LevelPrefab.GetComponent<BorderInfo>();
        levelConfig.LevelMarkersParent = levelConfig.LevelPrefab.GetComponentInChildren<LevelMarkersParent>().transform;
        levelConfig.CameraSpawnMarker = levelConfig.LevelPrefab.GetComponentInChildren<CameraSpawnMarker>();
      }

      EditorUtility.SetDirty(target);
    }
  }
}
