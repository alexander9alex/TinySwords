using Code.Gameplay.Common.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds.Registrars
{
  public class CastleAnimatorRegistrar : EntityComponentRegistrar
  {
    public AudioSource AudioSource;

    public override void RegisterComponents() =>
      Entity.AddAudioSource(AudioSource);

    public override void UnregisterComponents()
    {
      if (Entity.hasAudioSource)
        Entity.RemoveAudioSource();
    }
  }
}
