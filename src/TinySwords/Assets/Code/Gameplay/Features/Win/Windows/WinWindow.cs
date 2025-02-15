using System;
using Code.UI.Data;
using Code.UI.Windows;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Win.Windows
{
  internal class WinWindow : BaseWindow
  {
    public Button ContinueButton;
    private Action _continueGameAction;

    public void Construct() =>
      WindowId = WindowId.WinWindow;

    protected override void Initialize() =>
      ContinueButton.onClick.AddListener(ContinueGame);

    public void SetContinueGameAction(Action action) =>
      _continueGameAction = action;

    private void ContinueGame() =>
      _continueGameAction?.Invoke();
  }
}
