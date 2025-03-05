using Code.Gameplay.Features.Interactions.Highlight;
using Code.Gameplay.Features.Interactions.Select;
using Code.Gameplay.Features.Interactions.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Interactions
{
  public sealed class InteractionFeature : Feature
  {
    public InteractionFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateInteractionSystem>());
      Add(systems.Create<SetInteractionEndPositionSystem>());

      Add(systems.Create<CompleteInteractionSystem>());
      
      Add(systems.Create<HighlightFeature>());
      Add(systems.Create<SelectFeature>());
      
      Add(systems.Create<CleanupInteractionSystem>());
    }
  }
}
