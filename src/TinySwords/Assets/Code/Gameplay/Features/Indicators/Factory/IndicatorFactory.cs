using System;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Indicators.Configs;
using Code.Gameplay.Features.Indicators.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Indicators.Factory
{
  class IndicatorFactory : IIndicatorFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly ICameraProvider _cameraProvider;

    public IndicatorFactory(IStaticDataService staticData, ICameraProvider cameraProvider)
    {
      _staticData = staticData;
      _cameraProvider = cameraProvider;
    }

    public void CreateIndicator(GameEntity request)
    {
      IndicatorConfig config = _staticData.GetIndicatorConfig(request.IndicatorTypeId);

      GameEntity indicator = CreateEntity.Empty()
        .AddViewPrefab(config.IndicatorPrefab)
        .AddIndicatorTypeId(config.IndicatorTypeId)
        .AddWorldPosition(request.WorldPosition.RemoveZ())
        .AddSelfDestructTimer(config.IndicatorShowTime)
        .With(x => x.isIndicator = true)
        .With(x => x.isInitializationRequest = true)
        .With(x => x.isCreatedNow = true);

      switch (config.IndicatorTypeId)
      {
        case IndicatorTypeId.Attack:
          SetTargetId(request, indicator);
          break;
      }
    }

    private static void SetTargetId(GameEntity request, GameEntity indicator)
    {
      if (!request.hasTargetId)
        return;

      indicator
        .AddTargetId(request.TargetId)
        .With(x => x.isFollowToTarget = true);
    }
  }
}
