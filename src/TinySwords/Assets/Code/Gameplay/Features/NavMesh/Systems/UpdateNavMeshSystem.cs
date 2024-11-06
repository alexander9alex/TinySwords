using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.NavMesh.Systems
{
  public class UpdateNavMeshSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _navMeshCacheSources;
    private readonly IGroup<GameEntity> _updateNavMeshRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public UpdateNavMeshSystem(GameContext game)
    {
      _navMeshCacheSources = game.GetGroup(GameMatcher.NavMeshCollectSourcesCache);
      _updateNavMeshRequests = game.GetGroup(GameMatcher.UpdateNavMesh);
    }

    public void Execute()
    {
      foreach (GameEntity request in _updateNavMeshRequests.GetEntities(_buffer))
      foreach (GameEntity navMesh in _navMeshCacheSources)
      {
        navMesh.NavMeshCollectSourcesCache.UpdateNavMesh();

        request.isDestructed = true;
      }
    }
  }
}
