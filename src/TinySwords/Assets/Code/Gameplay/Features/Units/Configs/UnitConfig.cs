using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Unit Config", fileName = "UnitConfig", order = 0)]
  public class UnitConfig : ScriptableObject
  {
    public UnitTypeId TypeId;
    public TeamColor Color;

    [Header("Unit Setup")]
    public EntityBehaviour UnitPrefab;
    [Space]
    public float Hp;
    public float Damage;
    public float AttackRadius;
  }
}
