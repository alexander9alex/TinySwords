using Code.Gameplay.Common.Registrars;
using UnityEngine.AI;

namespace Code.Gameplay.Features.Move.Registrars
{
  public class NavMeshAgentRegistrar : EntityComponentRegistrar
  {
    public NavMeshAgent NavMeshAgent;

    public override void RegisterComponents() =>
      Entity.AddNavMeshAgent(NavMeshAgent);

    public override void UnregisterComponents()
    {
      if (Entity.hasNavMeshAgent)
        Entity.RemoveNavMeshAgent();
    }
  }
}
