using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input
{
  [Game] public class ScreenPosition : IComponent { public Vector2 Value; }
  [Game] public class MouseScreenPosition : IComponent { public Vector2 Value; }
  [Game] public class StartPosition : IComponent { public Vector2 Value; }
  [Game] public class EndPosition : IComponent { public Vector2 Value; }
  [Game] public class ActionStarted : IComponent { }
  [Game] public class ActionEnded : IComponent { }
}
