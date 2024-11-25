using UnityEngine;

namespace Code.Gameplay.Features.Animations.Animators
{
  public class DeathSkullAnimator : MonoBehaviour, IDeathAnimator
  {
    private const string HideSkullAnimation = "Skull_Hide";
    
    public Animator Animator;

    public void HideSkull() =>
      Animator.Play(HideSkullAnimation);
  }
}
