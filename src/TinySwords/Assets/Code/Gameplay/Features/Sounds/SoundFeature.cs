using Code.Gameplay.Features.Sounds.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Sounds
{
  public sealed class SoundFeature : Feature
  {
    public SoundFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateSoundSystem>());
      // Add(systems.Create<CreateSoundInPositionSystem>());

      Add(systems.Create<PlaySoundSystem>());
    }
  }
}
