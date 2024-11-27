using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Death.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Units/Unit Death", fileName = "UnitDeathConfig", order = 0)]
  public class UnitDeathConfig : ScriptableObject
  {
    public EntityBehaviour UnitDeathAnimationPrefab;
    public float DisplayTime;
    public float HideTime;
  }
}
