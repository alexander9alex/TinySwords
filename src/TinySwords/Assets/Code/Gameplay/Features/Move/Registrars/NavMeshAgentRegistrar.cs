using Code.Gameplay.Common.Registrars;
using UnityEngine.AI;

namespace Code.Gameplay.Features.Move.Registrars
{
  public class NavMeshAgentRegistrar : EntityComponentRegistrar
  {
    public NavMeshAgent NavMeshAgent;

    public int MoveAvoidancePriority;

    public override void RegisterComponents()
    {
      Entity.AddNavMeshAgent(NavMeshAgent)
        .AddCurrentAvoidancePriority(NavMeshAgent.avoidancePriority)
        .AddIdleAvoidancePriority(NavMeshAgent.avoidancePriority)
        .AddMoveAvoidancePriority(MoveAvoidancePriority);
    }

    public override void UnregisterComponents()
    {
      if (Entity.hasNavMeshAgent)
        Entity.RemoveNavMeshAgent();

      if (Entity.hasCurrentAvoidancePriority)
        Entity.RemoveCurrentAvoidancePriority();

      if (Entity.hasIdleAvoidancePriority)
        Entity.RemoveIdleAvoidancePriority();

      if (Entity.hasMoveAvoidancePriority)
        Entity.RemoveMoveAvoidancePriority();
    }
  }
}
