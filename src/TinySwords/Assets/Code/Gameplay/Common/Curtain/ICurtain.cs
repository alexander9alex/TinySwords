using System;

namespace Code.Gameplay.Common.Curtain
{
  public interface ICurtain
  {
    void Show(Action onEnded = null);
    void Hide(Action onEnded = null);
  }
}
