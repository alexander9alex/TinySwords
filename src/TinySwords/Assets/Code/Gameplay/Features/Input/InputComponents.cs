using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input
{
  [Game] public class LeftClick : IComponent { }
  [Game] public class LeftClickStarted : IComponent { }
  [Game] public class LeftClickEnded : IComponent { }
  
  [Game] public class RightClick : IComponent { }

  [Game] public class MousePosition : IComponent { public Vector2 Value; }
  [Game] public class MousePositionInput : IComponent { }
}
