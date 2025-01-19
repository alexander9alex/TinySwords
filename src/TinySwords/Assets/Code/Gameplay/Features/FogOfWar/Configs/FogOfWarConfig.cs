using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.FogOfWar.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Shaders/Fog Of War Config", fileName = "FogOfWarConfig", order = 0)]
  public class FogOfWarConfig : ScriptableObject
  {
    [Range(0, 1)]
    public float Intensity = 0.85f;
    
    [Range(0, 3)]
    public float VisibilitySmoothness = 1;

    public Material Material;
  }
}
