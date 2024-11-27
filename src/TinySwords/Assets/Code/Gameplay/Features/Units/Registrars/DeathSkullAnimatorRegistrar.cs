using Code.Gameplay.Common.Registrars;
using Code.Gameplay.Features.Death.Animators;

namespace Code.Gameplay.Features.Units.Registrars
{
  public class DeathSkullAnimatorRegistrar : EntityComponentRegistrar
  {
    public DeathSkullAnimator DeathSkullAnimator;
    public override void RegisterComponents()
    {
      Entity
        .AddDeathAnimator(DeathSkullAnimator);
    }

    public override void UnregisterComponents()
    {
      if (Entity.hasDeathAnimator)
        Entity.RemoveDeathAnimator();
    }
  }
}
