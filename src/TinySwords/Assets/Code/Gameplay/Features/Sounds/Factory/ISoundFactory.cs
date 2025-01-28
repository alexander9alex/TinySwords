using Code.Gameplay.Features.Sounds.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds.Factory
{
  public interface ISoundFactory
  {
    GameEntity CreateSound(SoundId soundId);
    GameEntity CreateSound(SoundId soundId, Vector2 pos);
    void CreateSoundDirectly(SoundId soundId);
  }
}
