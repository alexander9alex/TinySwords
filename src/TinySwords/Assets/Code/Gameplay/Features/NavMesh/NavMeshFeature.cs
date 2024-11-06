using Code.Gameplay.Features.NavMesh.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.NavMesh
{
  public sealed class NavMeshFeature : Feature
  {
    public NavMeshFeature(ISystemFactory systems)
    {
      Add(systems.Create<AddRootSourcesToNavMeshSystem>());
      Add(systems.Create<BuildNavMeshAtStartSystem>());
    }
  }
}
