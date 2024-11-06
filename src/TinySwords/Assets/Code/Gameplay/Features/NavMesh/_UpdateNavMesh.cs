using NavMeshPlus.Extensions;
using UnityEngine;

namespace Code
{
  public class _UpdateNavMesh : MonoBehaviour
  {
    public CollectSourcesCache2d CollectSourcesCache2d;

    void Update()
    {
      CollectSourcesCache2d.UpdateSource(gameObject);
    }
  }
}
