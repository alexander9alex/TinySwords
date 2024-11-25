using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Move
{
  [Game] public class Idle : IComponent { }
  [Game] public class Move : IComponent { }
  [Game] public class Movable : IComponent { }
  [Game] public class MoveDirection : IComponent { public Vector2 Value; }
  [Game] public class Destination : IComponent { public Vector2 Value; }
  [Game] public class EndDestination : IComponent { public Vector2 Value; }
  [Game] public class ChangeEndDestinationRequest : IComponent { }
}
