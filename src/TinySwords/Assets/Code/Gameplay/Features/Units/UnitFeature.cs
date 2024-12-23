using Code.Gameplay.Features.Units.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Units
{
  public sealed class UnitsFeature : Feature
  {
    public UnitsFeature(ISystemFactory systems)
    {
      Add(systems.Create<NotifyAlliesAboutTargetSystem>());
    }
  }
}
