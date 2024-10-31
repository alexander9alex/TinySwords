using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input
{
  [Game] public class MakeInteraction : IComponent { }
  [Game] public class MousePosition : IComponent { public Vector2 Value; }
}
