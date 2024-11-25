using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Units/Unit Config", fileName = "UnitConfig", order = 0)]
  public class UnitConfig : ScriptableObject
  {
    public UnitTypeId TypeId;
    public TeamColor Color;

    [Header("Unit Setup")]
    public EntityBehaviour UnitPrefab;
    [Space]
    public float Hp;
    public float Damage;
    public float Speed = 2;
    public float AttackReach;
    public float AttackCooldown = 1;
    public float CollectTargetRadius;
    public float MakeDecisionInterval = 0.5f;
    public float CollectTargetsInterval = 0.5f;
    public float CollectReachedTargetsInterval = 0.1f;
  }
}
