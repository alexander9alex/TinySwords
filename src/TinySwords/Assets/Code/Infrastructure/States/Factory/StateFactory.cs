using Code.Infrastructure.States.StateInfrastructure;
using Zenject;

namespace Code.Infrastructure.States.Factory
{
  class StateFactory : IStateFactory
  {
    private readonly DiContainer _container;

    public StateFactory(DiContainer container) =>
      _container = container;

    public TState GetState<TState>() where TState : class, IExitableState =>
      _container.Resolve<TState>();
  }
}
