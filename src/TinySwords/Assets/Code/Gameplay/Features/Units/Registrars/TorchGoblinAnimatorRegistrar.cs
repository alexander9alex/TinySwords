using Code.Gameplay.Common.Registrars;
using Code.Gameplay.Features.Units.Animations.Animators;

namespace Code.Gameplay.Features.Units.Registrars
{
  public class TorchGoblinAnimatorRegistrar : EntityComponentRegistrar
  {
    public TorchGoblinAnimator TorchGoblinAnimator;
    public override void RegisterComponents() =>
      Entity.AddMoveAnimator(TorchGoblinAnimator);

    public override void UnregisterComponents()
    {
      if (Entity.hasMoveAnimator)
        Entity.RemoveMoveAnimator();
    }
  }
}
