using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.MoveIndicator.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Indicators/Attack Indicator", fileName = "AttackIndicatorConfig", order = 0)]
  public class AttackIndicatorConfig : ScriptableObject
  {
    public EntityBehaviour IndicatorPrefab;
    public float IndicatorShowTime;
  }
}