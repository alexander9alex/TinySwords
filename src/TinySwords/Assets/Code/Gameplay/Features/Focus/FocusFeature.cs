using Code.Gameplay.Features.Focus.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Focus
{
  public sealed class FocusFeature : Feature
  {
    public FocusFeature(ISystemFactory systems)
    {
      Add(systems.Create<MarkUnfocusedSystem>());
      Add(systems.Create<MarkFocusedSystem>());
    }
  }
}
