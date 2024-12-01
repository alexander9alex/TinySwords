using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.MoveIndicator.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Move Indicator", fileName = "MoveIndicatorConfig", order = 0)]
  public class MoveIndicatorConfig : ScriptableObject
  {
    public EntityBehaviour IndicatorPrefab;
    public float IndicatorShowTime;
  }
}