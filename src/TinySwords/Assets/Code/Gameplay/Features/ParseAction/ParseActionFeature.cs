using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.ParseAction
{
  public sealed class ParseActionFeature : Feature
  {
    public ParseActionFeature(ISystemFactory systems)
    {
      Add(systems.Create<SelectActionSystem>());
    }
  }
}
