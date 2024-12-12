using Code.Gameplay.Common.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Registrars
{
  public class LegsPositionOffsetRegistrar : EntityComponentRegistrar
  {
    public Transform CenterPosition;
    public Transform LegsPosition;

    public override void RegisterComponents() =>
      Entity.AddLegsPositionOffset(LegsPosition.position - CenterPosition.position);

    public override void UnregisterComponents()
    {
      if (Entity.hasLegsPositionOffset)
        Entity.RemoveLegsPositionOffset();
    }
  }
}
