using Entitas;
using NavMeshPlus.Components;
using NavMeshPlus.Extensions;

namespace Code.Gameplay.Features.NavMesh
{
  [Game] public class NavMesh : IComponent { }
  [Game] public class NavMeshSurfaceComponent : IComponent { public NavMeshSurface Value; }
  [Game] public class NavMeshCollectSourcesCacheComponent : IComponent { public CollectSourcesCache2d Value; }
  [Game] public class NavMeshRootSourcesComponent : IComponent { public RootSources2d Value; }
  [Game] public class NavMeshCollectSourcesComponent : IComponent { public CollectSources2d Value; }
  [Game] public class BuildNavMeshAtStart : IComponent { }
  [Game] public class NotAddedNavMeshRootSource : IComponent { }
  
}
