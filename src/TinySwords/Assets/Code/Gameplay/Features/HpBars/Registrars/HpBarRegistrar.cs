using Code.Gameplay.Common.Registrars;
using Code.Gameplay.Features.HpBars.Behaviours;

namespace Code.Gameplay.Features.HpBars.Registrars
{
  public class HpBarRegistrar : EntityComponentRegistrar
  {
    public HpBar HpBar;

    public override void RegisterComponents() =>
      Entity.AddHpBar(HpBar);

    public override void UnregisterComponents()
    {
      if (Entity.hasHpBar)
        Entity.RemoveHpBar();
    }
  }
}
