using Code.Gameplay.Features.Interactions.Highlight.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Interactions.Highlight
{
  public sealed class HighlightFeature : Feature
  {
    public HighlightFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateHighlightSystem>());
      
      Add(systems.Create<CalculateHighlightLocationSystem>());
      Add(systems.Create<UpdateHighlightLocationSystem>());
      
      Add(systems.Create<DestroyHighlightSystem>());
    }
  }
}
