using Code.Gameplay.Features.Lose.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Lose
{
  public sealed class LoseFeature : Feature
  {
    public LoseFeature(ISystemFactory systems)
    {
      Add(systems.Create<LoseGameSystem>());
    }
  }
}
