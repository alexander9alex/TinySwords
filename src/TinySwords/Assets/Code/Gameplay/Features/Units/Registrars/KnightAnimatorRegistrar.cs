using Code.Gameplay.Common.Registrars;
using Code.Gameplay.Features.Units.Animations.Animators;

namespace Code.Gameplay.Features.Units.Registrars
{
  public class KnightAnimatorRegistrar : EntityComponentRegistrar
  {
    public KnightAnimator KnightAnimator;
    public override void RegisterComponents() =>
      Entity.AddSelectingAnimator(KnightAnimator);

    public override void UnregisterComponents()
    {
      if (Entity.hasSelectingAnimator)
        Entity.RemoveSelectingAnimator();
    }
  }
}
