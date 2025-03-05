using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input
{
  [Game] public class Input : IComponent { }
  [Game] public class MousePosition : IComponent { public Vector2 Value; }
  
  [Game] public class InteractionStartInput : IComponent { }
  [Game] public class InteractionEndInput : IComponent { }
  [Game] public class FastInteractionInput : IComponent { }

  [Game] public class ScreenPosition : IComponent { public Vector2 Value; }
  [Game] public class StartPosition : IComponent { public Vector2 Value; }
  [Game] public class EndPosition : IComponent { public Vector2 Value; }
  [Game] public class MoveCameraDirection : IComponent { public Vector2 Value; }
  
  [Game] public class ApplyCommandInput : IComponent { }
  [Game] public class CancelCommandInput : IComponent { }
}
