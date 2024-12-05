using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.IncorrectCommandIndicator.Configs;
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
      MoveIndicatorConfig config = _staticData.GetMoveIndicatorConfig(); // todo: refactor configs

      return CreateEntity.Empty()
        .AddViewPrefab(config.IndicatorPrefab)
        .AddWorldPosition(pos.RemoveZ())
        .AddSelfDestructTimer(config.IndicatorShowTime)
        .With(x => x.isMoveIndicator = true)
        .With(x => x.isInitializationRequest = true);
    }

    public GameEntity CreateAttackIndicator(Vector3 pos)
    {
      AttackIndicatorConfig config = _staticData.GetAttackIndicatorConfig();

      return CreateEntity.Empty()
        .AddViewPrefab(config.IndicatorPrefab)
        .AddWorldPosition(pos.RemoveZ())
        .AddSelfDestructTimer(config.IndicatorShowTime)
        .With(x => x.isAttackIndicator = true)
        .With(x => x.isInitializationRequest = true);
    }

    public GameEntity CreateIncorrectCommandIndicator(Vector3 pos)
    {
      IncorrectCommandIndicatorConfig config = _staticData.GetIncorrectCommandIndicatorConfig();

      return CreateEntity.Empty()
        .AddViewPrefab(config.IndicatorPrefab)
        .AddWorldPosition(pos.RemoveZ())
        .AddSelfDestructTimer(config.IndicatorShowTime)
        .With(x => x.isIncorrectCommandIndicator = true)
        .With(x => x.isInitializationRequest = true);
    }
  }
}
