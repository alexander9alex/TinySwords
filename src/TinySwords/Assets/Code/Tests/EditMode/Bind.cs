using Code.Gameplay.Services;
using Code.Gameplay.UtilityAI;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using Code.Infrastructure.Factory;
using Zenject;

namespace Code.Tests.EditMode
{
  public static class Bind
  {
    public static void GameContext(DiContainer diContainer) =>
      diContainer.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();

    public static void UnitAI(DiContainer diContainer)
    {
      diContainer.Bind<When>().To<When>().AsSingle();
      diContainer.Bind<GetInput>().To<GetInput>().AsSingle();
      diContainer.Bind<Score>().To<Score>().AsSingle();
      diContainer.Bind<IBrainsComponents>().To<BrainsComponents>().AsSingle();

      diContainer.Bind<UnitBrains>().To<UnitBrains>().AsSingle();
      diContainer.Bind<IUnitAI>().To<UnitAI>().AsSingle();
    }

    public static void SystemFactory(DiContainer diContainer) =>
      diContainer.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();

    public static void TimeService(DiContainer diContainer)
    {
      diContainer.Bind<ITimeService>().To<TimeService>().AsSingle();
    }
  }
}
