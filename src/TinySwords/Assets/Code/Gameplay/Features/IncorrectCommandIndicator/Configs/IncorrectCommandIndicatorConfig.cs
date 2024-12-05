using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.IncorrectCommandIndicator.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Indicators/Incorrect Command Indicator", fileName = "IncorrectCommandIndicatorConfig", order = 0)]
  public class IncorrectCommandIndicatorConfig : ScriptableObject
  {
    public EntityBehaviour IndicatorPrefab;
    public float IndicatorShowTime;
  }
}
