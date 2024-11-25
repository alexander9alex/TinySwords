using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.MoveIndicator.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Move Click Indicator", fileName = "MoveClickIndicatorConfig", order = 0)]
  public class MoveClickIndicatorConfig : ScriptableObject
  {
    public EntityBehaviour IndicatorPrefab;
    public float IndicatorShowTime;
  }
}