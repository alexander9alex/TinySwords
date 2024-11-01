using Code.Gameplay.Features.Animations.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Animations
{
  public sealed class AnimationFeature : Feature
  {
    public AnimationFeature(ISystemFactory systems)
    {
      Add(systems.Create<SelectAnimationSystem>());
      Add(systems.Create<UnselectAnimationSystem>());
    }
  }
}
