using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Dead.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.Dead.Factory
{
  class UnitDeathFactory : IUnitDeathFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly IIdentifierService _identifiers;

    public UnitDeathFactory(IStaticDataService staticData, IIdentifierService identifiers)
    {
      _staticData = staticData;
      _identifiers = identifiers;
    }

    public void CreateDeathAnimation(Vector3 pos)
    {
      UnitDeathConfig config = _staticData.GetUnitDeathConfig();

      CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddViewPrefab(config.UnitDeathAnimationPrefab)
        .AddWorldPosition(pos)
        .AddDisplayTimer(config.DisplayTime)
        .AddHideTimer(config.HideTime)
        .With(x => x.isInitializationRequest = true);
    }
  }
}
