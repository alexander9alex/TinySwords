using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.NavMesh.Systems
{
  public class BuildNavMeshAtStartSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _buildNavMeshAtStartRequests;
    private readonly IGroup<GameEntity> _navMeshSurfaces;
    private readonly List<GameEntity> _buffer = new(1);

    public BuildNavMeshAtStartSystem(GameContext game)
    {
      _buildNavMeshAtStartRequests = game.GetGroup(GameMatcher.BuildNavMeshAtStart);
      _navMeshSurfaces = game.GetGroup(GameMatcher.NavMeshSurface);
    }

    public void Execute()
    {
      foreach (GameEntity request in _buildNavMeshAtStartRequests.GetEntities(_buffer))
      foreach (GameEntity navMesh in _navMeshSurfaces)
      {
        navMesh.NavMeshSurface.BuildNavMesh();
        
        request.isDestructed = true;
      }
    }
  }
}
