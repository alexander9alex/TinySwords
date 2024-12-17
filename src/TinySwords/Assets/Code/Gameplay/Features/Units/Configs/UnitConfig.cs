using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Units/Unit Config", fileName = "UnitConfig", order = 0)]
  public class UnitConfig : ScriptableObject
  {
    [Header("Unit Type")]
    public UnitTypeId TypeId;
    public TeamColor Color;

    [Header("Unit Setup")]
    public EntityBehaviour UnitPrefab;
    [Space]
    public float Hp;
    public float Damage;
    public float Speed = 2;
    public float AttackReach = 0.85f;
    public float AttackCooldown = 1;
    
    [Space]
    public float CollectTargetRadius = 3;
    public float CollectAlliesRadius = 1;
    
    [Space]
    public float VisionRadius = 3;
    public float UpdateFieldOfVisionInterval = 0.5f;
  }
}
