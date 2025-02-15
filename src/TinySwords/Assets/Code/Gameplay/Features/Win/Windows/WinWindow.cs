using System;
using Code.UI.Data;
using Code.UI.Windows;
using Code.UI.Windows.Services;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.Win.Windows
{
  internal class WinWindow : BaseWindow
  {
    public Button ContinueButton;
    
    private IWindowService _windowService;
    private Action _continueGameAction;

    [Inject]
    private void Construct(IWindowService windowService)
    {
      WindowId = WindowId.WinWindow;

      _windowService = windowService;
    }

    protected override void Initialize() =>
      ContinueButton.onClick.AddListener(ContinueGame);

    public void SetContinueGameAction(Action action) =>
      _continueGameAction = action;

    private void ContinueGame()
    {
      _continueGameAction?.Invoke();
      _windowService.CloseWindow(WindowId);
    }
  }
}
