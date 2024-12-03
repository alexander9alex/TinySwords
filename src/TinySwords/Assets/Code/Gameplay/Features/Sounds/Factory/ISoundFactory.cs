using Code.Gameplay.Features.Sounds.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds.Factory
{
  public interface ISoundFactory
  {
    void CreateSound(SoundId soundId);
    void CreateSound(SoundId soundId, Vector2 pos);
  }
}
