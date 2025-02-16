using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Death.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.Death.Factory
{
  public class UnitDeathFactory : IUnitDeathFactory
  {
    private readonly IIdentifierService _identifiers;
    private readonly UnitDeathConfig _unitDeathConfig;

    public UnitDeathFactory(IStaticDataService staticData, IIdentifierService identifiers)
    {
      _identifiers = identifiers;
      
      _unitDeathConfig = staticData.GetUnitDeathConfig();
    }

    public void CreateDeathAnimation(Vector3 pos)
    {
      CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddViewPrefab(_unitDeathConfig.UnitDeathAnimationPrefab)
        .AddWorldPosition(pos)
        .AddDisplayTimer(_unitDeathConfig.DisplayTime)
        .AddHideTimer(_unitDeathConfig.HideTime)
        .With(x => x.isInitializationRequest = true);
    }
  }
}
