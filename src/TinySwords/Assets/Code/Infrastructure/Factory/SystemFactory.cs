using Entitas;
using Zenject;

namespace Code.Infrastructure.Factory
{
  public class SystemFactory : ISystemFactory
  {
    private readonly DiContainer _container;

    public SystemFactory(DiContainer container) =>
      _container = container;

    public TSystem Create<TSystem>() where TSystem : ISystem =>
      _container.Instantiate<TSystem>();

    public TSystem Create<TSystem>(params object[] args) where TSystem : ISystem =>
      _container.Instantiate<TSystem>(args);
  }
}
