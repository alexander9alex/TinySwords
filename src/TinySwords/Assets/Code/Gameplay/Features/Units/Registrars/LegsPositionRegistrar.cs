using Code.Gameplay.Common.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Units.Registrars
{
  public class LegsPositionRegistrar : EntityComponentRegistrar
  {
    public Transform LegsPosition;

    public override void RegisterComponents() =>
      Entity.AddLegsPosition(LegsPosition.position);

    public override void UnregisterComponents()
    {
      if (Entity.hasLegsPosition)
        Entity.RemoveLegsPosition();
    }
  }
}
