using Entitas;

namespace Code.Infrastructure.Factory
{
  public interface ISystemFactory
  {
    TSystem Create<TSystem>() where TSystem : ISystem;
    TSystem Create<TSystem>(params object[] args) where TSystem : ISystem;
  }
}
