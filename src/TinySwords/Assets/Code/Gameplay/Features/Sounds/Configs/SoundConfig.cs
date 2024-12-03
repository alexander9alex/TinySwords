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
    public AudioClip Clip;
    public float Volume = 1;
    public float Delay = 0;
    public float MinPitch = 0;
    public float MaxPitch = 0;
  }
}
