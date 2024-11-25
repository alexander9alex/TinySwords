using System.Collections;
using Code.Gameplay.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Animations.Animators
{
  public class DamageTakenAnimator : MonoBehaviour, IDamageTakenAnimator
  {
    public SpriteRenderer SpriteRenderer;
    public Color StartColor;
    public Color EndColor;
    public float AnimateTime = 0.8f;

    private ITimeService _time;
    private float HalfAnimateTime => AnimateTime / 2;
    private bool _alreadyAnimating;

    [Inject]
    private void Construct(ITimeService timeService) =>
      _time = timeService;

    public void AnimateDamage()
    {
      if (_alreadyAnimating)
        return;

      StartCoroutine(AnimateDamageCoroutine());
    }

    private IEnumerator AnimateDamageCoroutine()
    {
      _alreadyAnimating = true;
      float timer = 0;

      while (SpriteRenderer.color != EndColor)
      {
        SpriteRenderer.color = Color.Lerp(StartColor, EndColor, timer / HalfAnimateTime);
        timer += _time.DeltaTime;
        yield return null;
      }

      timer = 0;
      
      while (SpriteRenderer.color != StartColor)
      {
        SpriteRenderer.color = Color.Lerp(EndColor, StartColor, timer / HalfAnimateTime);
        timer += _time.DeltaTime;
        yield return null;
      }

      _alreadyAnimating = false;
    }
  }
}
