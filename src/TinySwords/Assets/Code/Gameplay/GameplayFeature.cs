using Code.Gameplay.Features.Animations;
using Code.Gameplay.Features.Move;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Views;

namespace Code.Gameplay
{
  public sealed class GameplayFeature : Feature
  {
    public GameplayFeature(ISystemFactory systems)
    {
      Add(systems.Create<BindViewFeature>());
      
      Add(systems.Create<MoveFeature>());
      
      Add(systems.Create<AnimationFeature>());
    }
  }
}
