using Code.Gameplay.Features.Sounds.Data;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Sound Config", fileName = "SoundConfig", order = 0)]
  public class SoundConfig : ScriptableObject
  {
    public SoundId SoundId;
    
    public EntityBehaviour SoundPrefab;
  }
}
