using Code.Gameplay.Common.Registrars;
using Code.Gameplay.Features.Animations.Animators;

namespace Code.Gameplay.Features.Units.Registrars
{
  public class DamageTakenAnimatorRegistrar : EntityComponentRegistrar
  {
    public DamageTakenAnimator DamageTakenAnimator;
    public override void RegisterComponents()
    {
      Entity
        .AddDamageTakenAnimator(DamageTakenAnimator);
    }

    public override void UnregisterComponents()
    {
      if (Entity.hasDamageTakenAnimator)
        Entity.RemoveDamageTakenAnimator();
    }
  }
}
