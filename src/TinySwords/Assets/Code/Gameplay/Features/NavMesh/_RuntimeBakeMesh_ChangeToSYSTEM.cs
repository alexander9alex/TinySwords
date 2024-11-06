using NavMeshPlus.Components;
using UnityEngine;

namespace Code
{
  public class _RuntimeBakeMesh_ChangeToSYSTEM : MonoBehaviour //todo
  {
    public NavMeshSurface Surface2D;

    public void Start()
    {
      // todo: build initial nav mesh 
      Surface2D.BuildNavMesh();
    }
  }
}
