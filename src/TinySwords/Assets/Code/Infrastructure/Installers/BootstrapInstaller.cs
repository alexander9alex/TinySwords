using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public override void InstallBindings()
    {
      BindStateFactory();
      BindGameStates();
      BindStateMachine();
    }

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

    public void Initialize()
    {
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
    }
  }
}
