using Code.Gameplay.Common.Registrars;
using NavMeshPlus.Components;
using NavMeshPlus.Extensions;

namespace Code.Gameplay.Features.NavMesh.Registrars
{
  public class NavMeshRegistrar : EntityComponentRegistrar
  {
    public NavMeshSurface NavMeshSurface;
    public CollectSourcesCache2d CollectSourcesCache;
    public RootSources2d RootSources;
    public CollectSources2d CollectSources;
    
    public override void RegisterComponents()
    {
      Entity.AddNavMeshSurface(NavMeshSurface);
      Entity.AddNavMeshCollectSourcesCache(CollectSourcesCache);
      Entity.AddNavMeshRootSources(RootSources);
      Entity.AddNavMeshCollectSources(CollectSources);
    }

    public override void UnregisterComponents()
    {
      if (Entity.hasNavMeshSurface)
        Entity.RemoveNavMeshSurface();

      if (Entity.hasNavMeshCollectSourcesCache)
        Entity.RemoveNavMeshCollectSourcesCache();
      
      if (Entity.hasNavMeshRootSources)
        Entity.RemoveNavMeshRootSources();

      if (Entity.hasNavMeshCollectSources)
        Entity.RemoveNavMeshCollectSources();
    }
  }
}
