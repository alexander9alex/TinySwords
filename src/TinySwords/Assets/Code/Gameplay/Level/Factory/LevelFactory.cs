using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Level.Configs;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Level.Factory
{
  public class LevelFactory : ILevelFactory
  {
    private readonly IStaticDataService _staticData;

    public LevelFactory(IStaticDataService staticData)
    {
      _staticData = staticData;
    }

    public void CreateLevel()
    {
      LevelConfig config = _staticData.GetLevelConfig();

      CreateEntity.Empty()
        .AddViewPrefab(config.MapPrefab)
        .AddWorldPosition(Vector3.zero)
        .With(x => x.isInitializationRequest = true)
        .With(x => x.isNotAddedNavMeshRootSource = true);
      
      CreateEntity.Empty()
        .With(x => x.isBuildNavMeshAtStart = true);
    }
  }
}
