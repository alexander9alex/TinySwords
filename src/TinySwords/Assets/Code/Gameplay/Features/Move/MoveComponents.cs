using Entitas;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Gameplay.Features.Move
{
  [Game] public class TransformComponent : IComponent { public Transform Value; }
  [Game] public class WorldPosition : IComponent { public Vector3 Value; }
  [Game] public class UpdatePositionAfterSpawning : IComponent { }
  [Game] public class NavMeshAgentComponent : IComponent { public NavMeshAgent Value; }
  [Game] public class PositionUpdated : IComponent { }
}
