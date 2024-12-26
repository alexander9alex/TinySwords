using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Camera Config", fileName = "CameraConfig", order = 0)]
  public class CameraConfig : ScriptableObject
  {
    public float MinScaling = 3;
    public float MaxScaling = 10;
    public float ScaleSpeed = 1;
  }
}
