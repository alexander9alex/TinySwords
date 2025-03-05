using Code.Gameplay.Features.FastInteractions.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.FastInteractions
{
  public sealed class FastInteractionFeature : Feature
  {
    public FastInteractionFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateFastInteractionSystem>());
      
      Add(systems.Create<AimedAttackFastInteractionSystem>());
      Add(systems.Create<MoveFastInteractionSystem>());
      Add(systems.Create<ProcessIncorrectFastInteractionSystem>());
      
      Add(systems.Create<CleanupFastInteractionSystem>());
    }
  }
}
