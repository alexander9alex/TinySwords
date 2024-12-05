using Code.Gameplay.Features.Indicators.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Indicators
{
  public sealed class IndicatorFeature : Feature
  {
    public IndicatorFeature(ISystemFactory systems)
    {
      Add(systems.Create<ConvertPositionOnCreateIndicatorRequestSystem>());
      Add(systems.Create<CreateIndicatorSystem>());
      
      Add(systems.Create<DestructOldIndicatorSystem>());
      
      Add(systems.Create<CleanupCreatedNowIndicatorSystem>());
    }
  }
}
