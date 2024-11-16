using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input
{
  [Game] public class PositionOnScreen : IComponent { public Vector2 Value; }
  [Game] public class MousePositionOnScreen : IComponent { public Vector2 Value; }
  [Game] public class StartPosition : IComponent { public Vector2 Value; }
  [Game] public class EndPosition : IComponent { public Vector2 Value; }
  
}
