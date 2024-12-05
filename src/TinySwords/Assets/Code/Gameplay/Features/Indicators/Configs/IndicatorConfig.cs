using Code.Gameplay.Features.Indicators.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Indicators.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Indicator Config", fileName = "IndicatorConfig", order = 0)]
  public class IndicatorConfig : ScriptableObject
  {
    public IndicatorTypeId IndicatorTypeId;
    public EntityBehaviour IndicatorPrefab;
    public float IndicatorShowTime;
  }
}