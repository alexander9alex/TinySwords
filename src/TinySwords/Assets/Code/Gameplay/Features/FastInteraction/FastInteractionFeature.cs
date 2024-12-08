using Code.Gameplay.Features.FastInteraction.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.FastInteraction
{
  public sealed class FastInteractionFeature : Feature
  {
    public FastInteractionFeature(ISystemFactory systems)
    {
      Add(systems.Create<AimedAttackFastInteractionSystem>());
      Add(systems.Create<MoveFastInteractionSystem>());

      Add(systems.Create<CleanupFastInteractionRequestSystem>());
    }
  }
}
