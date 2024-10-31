using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Feature.Units.Factory;
using Code.Gameplay.Services;
using Code.Infrastructure.Common.CoroutineRunner;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable, ICoroutineRunner
  {
    public Curtain Curtain;

    public override void InstallBindings()
    {
      BindInfrastructureServices();
      BindGameplayServices();
      BindGameplayFactories();
      BindStateFactory();
      BindGameStates();
      BindStateMachine();
    }

    private void BindInfrastructureServices()
    {
      Container.BindInterfacesAndSelfTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }

    private void BindGameplayServices()
    {
      Container.Bind<ICurtain>().FromInstance(Curtain).AsSingle();
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
    }

    private void BindGameplayFactories()
    {
      Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();
    }

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingMainMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingGameState>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
    }

    private void BindStateFactory() =>
      Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();

    private void BindStateMachine() =>
      Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

    public void Initialize() =>
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
  }
}
