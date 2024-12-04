using Code.Gameplay.Features.Sounds.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds.Services
{
  public interface ISoundService
  {
    void PlayTakingDamageSound(GameEntity entity);
    void PlayMakeDamageSound(GameEntity entity);
    void PlaySound(SoundId soundId);
    void PlaySound(SoundId soundId, Vector3 pos);
    void PlaySoundDirectly(SoundId soundId);
  }
}
