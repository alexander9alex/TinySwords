using UnityEngine;
using UnityEngine.AI;

namespace Code.Gameplay.NavMeshPlus
{
  public class TurnOffUpdateRotationAndUpAxisOnNavMeshPlus : MonoBehaviour
  {
    private void Awake()
    {
      NavMeshAgent agent = GetComponent<NavMeshAgent>();
      agent.updateRotation = false;
      agent.updateUpAxis = false;
    }
  }
}
