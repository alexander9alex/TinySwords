using Code.Gameplay.Features.Sounds.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Sounds
{
  public sealed class SoundFeature : Feature
  {
    public SoundFeature(ISystemFactory systems)
    {
      Add(systems.Create<TickToSoundDelaySystem>());
      
      Add(systems.Create<CreateSoundSystem>());
      Add(systems.Create<CreateSoundInPositionSystem>());
      
      Add(systems.Create<SetAudioClipSystem>());
      Add(systems.Create<SetVolumeSystem>());
      Add(systems.Create<SetRandomPitchSystem>());

      Add(systems.Create<PlaySoundSystem>());
      
      Add(systems.Create<CleanupInitializeSoundSystem>());
    }
  }
}
