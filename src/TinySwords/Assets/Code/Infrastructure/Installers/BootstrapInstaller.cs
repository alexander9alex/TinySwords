using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.StateMachine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindStateFactory();
      BindStateMachine();
    }

    private void BindStateFactory() =>
      Container.Bind<IStateFactory>().To<StateFactory>().AsCached();

    private void BindStateMachine() =>
      Container.Bind<IGameStateMachine>().To<IGameStateMachine>().AsCached();
  }
}
