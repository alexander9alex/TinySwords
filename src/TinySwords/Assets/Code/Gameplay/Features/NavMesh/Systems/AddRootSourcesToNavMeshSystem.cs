using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.NavMesh.Systems
{
  public class AddRootSourcesToNavMeshSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _navMeshRootSources;
    private readonly IGroup<GameEntity> _notAddedRootSources;
    private readonly List<GameEntity> _buffer = new(1);

    public AddRootSourcesToNavMeshSystem(GameContext game)
    {
      _navMeshRootSources = game.GetGroup(GameMatcher.NavMeshRootSources);
      _notAddedRootSources = game.GetGroup(GameMatcher.AllOf(GameMatcher.NotAddedNavMeshRootSource, GameMatcher.View));
    }

    public void Execute()
    {
      foreach (GameEntity rootSource in _notAddedRootSources.GetEntities(_buffer))
      foreach (GameEntity navMesh in _navMeshRootSources)
      {
        navMesh.NavMeshRootSources.RootSources.Add(rootSource.View.gameObject);

        CreateEntity.Empty()
          .With(x => x.isBuildNavMeshAtStart = true);

        rootSource.isNotAddedNavMeshRootSource = false;
      }
    }
  }
}
