using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input
{
  [Game] public class ScreenPosition : IComponent { public Vector2 Value; }
  [Game] public class MouseScreenPosition : IComponent { public Vector2 Value; }
  [Game] public class StartPosition : IComponent { public Vector2 Value; }
  [Game] public class EndPosition : IComponent { public Vector2 Value; }
  [Game] public class InteractionStarted : IComponent { }
  [Game] public class InteractionEnded : IComponent { }
}
