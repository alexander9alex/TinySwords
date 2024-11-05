using Code.Gameplay.Features.Units.Animations.Animators;
using UnityEngine;

namespace Code.Gameplay.Features.Build.Animators
{
  public class CastleAnimator : MonoBehaviour, ISelectingAnimator
  {
    public GameObject SelectingCircle;

    public void AnimateSelecting() =>
      SelectingCircle.SetActive(true);

    public void AnimateUnselecting() =>
      SelectingCircle.SetActive(false);
  }
}
