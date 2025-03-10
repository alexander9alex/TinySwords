using System;
using System.Collections;
using Code.Infrastructure.Common.CoroutineRunner;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Common.Curtain
{
  public class Curtain : MonoBehaviour, ICurtain
  {
    private const int Visible = 1;
    private const int Invisible = 0;
    
    public float DisplaySpeedInSec;
    public Image CurtainImage;

    private ICoroutineRunner _coroutineRunner;

    [Inject]
    private void Construct(ICoroutineRunner coroutineRunner) =>
      _coroutineRunner = coroutineRunner;

    public void Show(Action onEnded = null)
    {
      CurtainImage.raycastTarget = true;
      _coroutineRunner.StartCoroutine(ChangeCurtainFade(Visible, onEnded));
    }

    public void Hide(Action onEnded = null)
    {
      onEnded += () => CurtainImage.raycastTarget = false;
      _coroutineRunner.StartCoroutine(ChangeCurtainFade(Invisible, onEnded));
    }

    private IEnumerator ChangeCurtainFade(int endFade, Action onEnded)
    {
      Color startColor = CurtainImage.color;
      Color endColor = new Color(startColor.r, startColor.g, startColor.b, endFade);

      float timer = 0f;

      while (CurtainImage.color != endColor)
      {
        CurtainImage.color = NextColor(startColor, endColor, timer);
        yield return null;
        
        timer += Time.deltaTime;
      }
      
      onEnded?.Invoke();
    }

    private Color NextColor(Color startColor, Color endColor, float timer) =>
      Color.Lerp(startColor, endColor, timer / DisplaySpeedInSec);
  }
}
