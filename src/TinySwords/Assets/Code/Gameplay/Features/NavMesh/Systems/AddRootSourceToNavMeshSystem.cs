using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.NavMesh.Systems
{
  public class AddRootSourceToNavMeshSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _navMeshRootSources;
    private readonly IGroup<GameEntity> _notAddedRootSources;
    private readonly List<GameEntity> _buffer = new(1);

    public AddRootSourceToNavMeshSystem(GameContext game)
    {
      _navMeshRootSources = game.GetGroup(GameMatcher.AllOf(GameMatcher.NavMeshRootSources, GameMatcher.NavMeshCollectSourcesCache));
      _notAddedRootSources = game.GetGroup(GameMatcher.AllOf(GameMatcher.NotAddedNavMeshRootSource, GameMatcher.View));
    }

    public void Execute()
    {
      foreach (GameEntity rootSource in _notAddedRootSources.GetEntities(_buffer))
      foreach (GameEntity navMesh in _navMeshRootSources)
      {
        navMesh.NavMeshRootSources.RootSources.Add(rootSource.View.gameObject);
        
        rootSource.isNotAddedNavMeshRootSource = false;
      }
    }
  }
}
