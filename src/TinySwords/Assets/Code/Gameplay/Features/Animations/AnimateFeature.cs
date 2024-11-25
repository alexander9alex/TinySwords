using Code.Gameplay.Features.Animations.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Animations
{
  public sealed class AnimateFeature : Feature
  {
    public AnimateFeature(ISystemFactory systems)
    {
      Add(systems.Create<InitializeAttackAnimatorSystem>());
      
      Add(systems.Create<AnimateSelectingSystem>());
      Add(systems.Create<AnimateUnselectingSystem>());
      
      Add(systems.Create<AnimateAttackSystem>());
      
      Add(systems.Create<AnimateIdleSystem>());
      Add(systems.Create<AnimateMoveSystem>());
      Add(systems.Create<UpdateLookDirectionSystem>());
      
      Add(systems.Create<AnimateUnitDeadSystem>());
    }
  }
}
