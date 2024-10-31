using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Unit Config", fileName = "UnitConfig", order = 0)]
  public class UnitConfig : ScriptableObject
  {
    public UnitTypeId TypeId;
    public UnitColor Color;

    [Header("Unit Setup")]
    public GameObject UnitPrefab;
    [Space]
    public float Hp;
    public float Damage;
    public float AttackRadius;
  }
}
