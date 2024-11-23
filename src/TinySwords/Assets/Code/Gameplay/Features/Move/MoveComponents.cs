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
  [Game] public class Idle : IComponent { }
  [Game] public class Move : IComponent { }
  [Game] public class Movable : IComponent { }
  [Game] public class MoveDirection : IComponent { public Vector2 Value; }
  [Game] public class Destination : IComponent { public Vector2 Value; }
  [Game] public class EndDestination : IComponent { public Vector2 Value; }
  
  [Game] public class IdleAvoidancePriority : IComponent { public int Value; }
  [Game] public class MoveAvoidancePriority : IComponent { public int Value; }
  [Game] public class CurrentAvoidancePriority : IComponent { public int Value; }
  
  [Game] public class MoveRequest : IComponent { }
  [Game] public class MoveClickIndicator : IComponent { }
  [Game] public class CreatedNow : IComponent { }
  [Game] public class DestructOldMoveIndicatorRequest : IComponent { }
  
  [Game] public class ChangeEndDestinationRequest : IComponent { }
}
