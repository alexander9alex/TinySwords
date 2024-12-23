using Code.Gameplay.Common.Registrars;
using Code.Gameplay.Features.Units.Animators;

namespace Code.Gameplay.Features.Units.Registrars
{
  public class KnightAnimatorRegistrar : EntityComponentRegistrar
  {
    public KnightAnimator KnightAnimator;
    public override void RegisterComponents()
    {
      Entity
        .AddSelectingAnimator(KnightAnimator)
        .AddMoveAnimator(KnightAnimator)
        .AddAttackAnimator(KnightAnimator)
        .AddAnimationSpeedChanger(KnightAnimator);
    }

    public override void UnregisterComponents()
    {
      if (Entity.hasSelectingAnimator)
        Entity.RemoveSelectingAnimator();

      if (Entity.hasMoveAnimator)
        Entity.RemoveMoveAnimator();
      
      if (Entity.hasAttackAnimator)
        Entity.RemoveAttackAnimator();

      if (Entity.hasAnimationSpeedChanger)
        Entity.RemoveAnimationSpeedChanger();
    }
  }
}
