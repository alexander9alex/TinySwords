using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Gameplay.Features.NavMesh.Systems
{
  public class AddSourceToNavMeshCachedSourcesSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _navMeshRootSources;
    private readonly IGroup<GameEntity> _addToNavMeshCachedSourcesRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public AddSourceToNavMeshCachedSourcesSystem(GameContext game)
    {
      _navMeshRootSources = game.GetGroup(GameMatcher.AllOf(GameMatcher.NavMeshRootSources, GameMatcher.NavMeshCollectSourcesCache));
      _addToNavMeshCachedSourcesRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.AddToNavMeshCachedSources, GameMatcher.View));
    }

    public void Execute()
    {
      foreach (GameEntity rootSource in _addToNavMeshCachedSourcesRequests.GetEntities(_buffer))
      foreach (GameEntity navMesh in _navMeshRootSources)
      {
        GameObject viewGo = rootSource.View.gameObject;
        
        NavMeshBuildSource src = new();
        src.transform = viewGo.transform.localToWorldMatrix;
        src.shape = NavMeshBuildSourceShape.Box;
        src.size = viewGo.GetComponent<BoxCollider2D>().size.SetZ(1);
        
        navMesh.NavMeshCollectSourcesCache.AddSource(viewGo, src);
        
        CreateEntity.Empty()
          .With(x => x.isUpdateNavMesh = true);

        rootSource.isAddToNavMeshCachedSources = false;
      }
    }
  }
}
