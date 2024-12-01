using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.MoveIndicator.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.MoveIndicator.Factory
{
  class IndicatorFactory : IIndicatorFactory
  {
    private readonly IStaticDataService _staticData;

    public IndicatorFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public GameEntity CreateMoveIndicator(Vector3 pos)
    {
      MoveIndicatorConfig config = _staticData.GetMoveIndicatorConfig();

      return CreateEntity.Empty()
        .AddViewPrefab(config.IndicatorPrefab)
        .AddWorldPosition(pos.RemoveZ())
        .AddSelfDestructTimer(config.IndicatorShowTime)
        .With(x => x.isMoveIndicator = true)
        .With(x => x.isInitializationRequest = true);
    }
  }
}
