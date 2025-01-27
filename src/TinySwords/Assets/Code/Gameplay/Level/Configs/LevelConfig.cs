using System.Collections.Generic;
using Code.Gameplay.Features.FogOfWar.Data;
using Code.Gameplay.Level.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Level.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Level Config", fileName = "LevelConfig", order = 0)]
  public class LevelConfig : ScriptableObject
  {
    public LevelId LevelId;

    [Header("Fill in for collect level setup")]
    public EntityBehaviour LevelPrefab;
    
    [Header("Level Setup")]
    public BorderInfo BorderInfo;
    public Transform LevelMarkersParent;
    public List<FogOfWarMarker> FogOfWarMarkers;
  }
}
