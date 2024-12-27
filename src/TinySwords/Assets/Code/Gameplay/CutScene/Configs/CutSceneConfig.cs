using System.Collections.Generic;
using Code.Gameplay.CutScene.Data;
using UnityEngine;

namespace Code.Gameplay.CutScene.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Cut Scene Config", fileName = "CutSceneConfig", order = 0)]
  public class CutSceneConfig : ScriptableObject
  {
    public CutSceneId CutSceneId;
    public GameObject CutScenePrefab;
    public List<string> Replicas;
    public float ReplicaSymbolDisplaySpeed = 0.05f;
    public float ReplicaEndSentenceDisplaySpeed = 0.5f;
    public float TimeToHideReplica = 1;
    public float TimeToWaitNextReplica = 0.5f;
  }
}