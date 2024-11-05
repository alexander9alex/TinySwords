using Code.Gameplay.Features.Units.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Build.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Castle Config", fileName = "CastleConfig", order = 0)]
  public class CastleConfig : ScriptableObject
  {
    public TeamColor Color;
    
    public EntityBehaviour CastlePrefab;

    public float MaxHp;
    // todo: public List<SpawningUnitData> with price and prefab
  }
}