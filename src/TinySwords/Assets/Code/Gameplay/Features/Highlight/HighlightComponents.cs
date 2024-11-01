using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Highlight
{
  [Game] public class Highlight : IComponent { }
  [Game] public class StartPosition : IComponent { public Vector2 Value; }
  [Game] public class EndPosition : IComponent { public Vector2 Value; }
  [Game] public class RectTransformComponent : IComponent { public RectTransform Value; }
}
