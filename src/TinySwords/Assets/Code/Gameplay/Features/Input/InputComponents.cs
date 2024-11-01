using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input
{
  [Game] public class LeftClick : IComponent { }
  [Game] public class MousePosition : IComponent { public Vector2 Value; }
}
