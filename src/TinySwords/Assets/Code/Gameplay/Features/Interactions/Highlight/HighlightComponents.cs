using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interactions.Highlight
{
  [Game] public class Highlight : IComponent { }
  [Game] public class HighlightEnded : IComponent { }
  [Game] public class CreateHighlightRequest : IComponent { }
  [Game] public class CenterPosition : IComponent { public Vector2 Value; }
  [Game] public class Size : IComponent { public Vector2 Value; }
  [Game] public class RectTransformComponent : IComponent { public RectTransform Value; }
}
