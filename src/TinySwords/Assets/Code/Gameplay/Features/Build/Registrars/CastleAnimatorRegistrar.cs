using Code.Gameplay.Common.Registrars;
using Code.Gameplay.Features.Build.Animators;

namespace Code.Gameplay.Features.Build.Registrars
{
  public class CastleAnimatorRegistrar : EntityComponentRegistrar
  {
    public CastleAnimator CastleAnimator;

    public override void RegisterComponents() =>
      Entity.AddSelectingAnimator(CastleAnimator);

    public override void UnregisterComponents()
    {
      if (Entity.hasSelectingAnimator)
        Entity.RemoveSelectingAnimator();
    }
  }
}
