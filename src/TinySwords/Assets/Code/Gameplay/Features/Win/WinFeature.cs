using Code.Gameplay.Features.Win.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Win
{
  public sealed class WinFeature : Feature
  {
    public WinFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateKillToWinConditionSystem>());
      
      Add(systems.Create<CheckKillToWinConditionSystem>());
      
      Add(systems.Create<CleanupCreateWinConditionRequestSystem>());
    }
  }
}
