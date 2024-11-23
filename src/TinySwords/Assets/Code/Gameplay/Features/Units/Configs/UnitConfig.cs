using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;
using UnityEngine;
using UnityEngine.Serialization;

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
    public float CollectTargetRadius;
    public float MakeDecisionInterval = 0.5f;
    public float CollectTargetsInterval = 0.5f;
  }
}
