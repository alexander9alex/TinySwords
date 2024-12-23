using Code.Gameplay.Common.Registrars;
using Code.Gameplay.Features.Units.Animators;

namespace Code.Gameplay.Features.Units.Registrars
{
  public class TorchGoblinAnimatorRegistrar : EntityComponentRegistrar
  {
    public TorchGoblinAnimator TorchGoblinAnimator;
    public override void RegisterComponents()
    {
      Entity
        .AddMoveAnimator(TorchGoblinAnimator)
        .AddAttackAnimator(TorchGoblinAnimator)
        .AddAnimationSpeedChanger(TorchGoblinAnimator);
    }

    public override void UnregisterComponents()
    {
      if (Entity.hasMoveAnimator)
        Entity.RemoveMoveAnimator();

      if (Entity.hasAttackAnimator)
        Entity.RemoveAttackAnimator();

      if (Entity.hasAnimationSpeedChanger)
        Entity.RemoveAnimationSpeedChanger();
    }
  }
}
