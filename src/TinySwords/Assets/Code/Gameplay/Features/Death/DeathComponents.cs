using Code.Gameplay.Features.Death.Animators;
using Entitas;

namespace Code.Gameplay.Features.Death
{
  [Game] public class AnimateDeath : IComponent { }
  [Game] public class DeathAnimator : IComponent { public IDeathAnimator Value; }
  [Game] public class DisplayTimer : IComponent { public float Value; }
  [Game] public class HideTimer : IComponent { public float Value; }
}
