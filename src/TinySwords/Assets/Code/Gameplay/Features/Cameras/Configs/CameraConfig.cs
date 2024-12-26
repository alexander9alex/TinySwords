using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Camera Config", fileName = "CameraConfig", order = 0)]
  public class CameraConfig : ScriptableObject
  {
    [Header("Scale")]
    public float MinScaling = 3;
    public float MaxScaling = 10;
    public float ScaleStep = 1;
    public float SmoothScaleTolerance = 0.05f;
    
    [Range(0, 1)]
    public float ScaleSmoothness = .5f;
    
    [Header("Movement")]
    public float MovementSpeed = 1;
    public float CameraScaleOnMovementInfluenceCoefficient = 1;
  }
}
