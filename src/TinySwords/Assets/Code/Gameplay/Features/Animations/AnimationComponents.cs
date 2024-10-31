using Code.Gameplay.Features.Animations.Animators;
using Entitas;

namespace Code.Gameplay.Features.Animations
{
  [Game] public class Selected : IComponent { }
  [Game] public class SelectingAnimator : IComponent { public ISelectingAnimator Value; }
}
