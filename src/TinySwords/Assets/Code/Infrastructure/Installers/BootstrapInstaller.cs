using Code.Gameplay.Common.Curtain;
using Code.Infrastructure.Common.CoroutineRunner;
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
      BindStateFactory();
      BindGameStates();
      BindStateMachine();
    }

    private void BindInfrastructureServices() =>
      Container.BindInterfacesAndSelfTo<BootstrapInstaller>().FromInstance(this).AsSingle();

    private void BindGameplayServices() =>
      Container.Bind<ICurtain>().FromInstance(Curtain).AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadMainMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadGameState>().AsSingle();
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
