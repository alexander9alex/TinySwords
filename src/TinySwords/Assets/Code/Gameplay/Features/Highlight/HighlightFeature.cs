using Code.Gameplay.Features.Highlight.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Highlight
{
  public sealed class HighlightFeature : Feature
  {
    public HighlightFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateHighlightSystem>());
      Add(systems.Create<CalculateHighlightViewLocationSystem>());
      Add(systems.Create<ChangeHighlightViewLocationSystem>());
      
      Add(systems.Create<CleanupHighlightSystem>());
    }
  }
}
