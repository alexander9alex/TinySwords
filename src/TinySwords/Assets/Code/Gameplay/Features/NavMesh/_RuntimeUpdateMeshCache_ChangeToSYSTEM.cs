using NavMeshPlus.Extensions;
using UnityEngine;

namespace Code
{
  class _RuntimeUpdateMeshCache_ChangeToSYSTEM : MonoBehaviour //todo
  {
    public CollectSourcesCache2d cacheSources2D;

    private void Update()
    {
      if (cacheSources2D.IsDirty)
      {
        cacheSources2D.UpdateNavMesh();
      }
    }
  }
}
