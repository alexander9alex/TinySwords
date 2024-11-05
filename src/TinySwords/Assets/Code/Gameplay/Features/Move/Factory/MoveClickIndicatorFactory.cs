using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Move.Configs;
using Code.Gameplay.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Factory
{
  class MoveClickIndicatorFactory : IMoveClickIndicatorFactory
  {
    private readonly IStaticDataService _staticData;

    public MoveClickIndicatorFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public GameEntity CreateMoveIndicator(Vector3 pos)
    {
      MoveClickIndicatorConfig config = _staticData.GetMoveClickIndicatorConfig();

      return CreateEntity.Empty()
        .AddViewPrefab(config.IndicatorPrefab)
        .AddWorldPosition(pos.RemoveZ())
        .AddSelfDestructTimer(config.IndicatorShowTime)
        .With(x => x.isMoveClickIndicator = true)
        .With(x => x.isUpdatePositionAfterSpawning = true);
    }
  }
}
