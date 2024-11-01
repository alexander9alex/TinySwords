using Code.Gameplay.Features.Units.Animations.Animators;
using Entitas;

namespace Code.Gameplay.Features.Units
{
  [Game] public class Unit : IComponent { }
  [Game] public class MoveAnimator : IComponent { public IMoveAnimator Value; }
}
