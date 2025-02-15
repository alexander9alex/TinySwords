using System;
using Code.UI.Data;
using Code.UI.Windows;
using Code.UI.Windows.Services;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.Lose.Windows
{
  public class LoseWindow : BaseWindow
  {
    public Button RestartGameButton;

    private IWindowService _windowService;
    private Action _restartGameAction;

    [Inject]
    private void Construct(IWindowService windowService)
    {
      WindowId = WindowId.LoseWindow;

      _windowService = windowService;
    }

    protected override void Initialize() =>
      RestartGameButton.onClick.AddListener(RestartGame);

    public void SetRestartAction(Action action) =>
      _restartGameAction = action;

    private void RestartGame()
    {
      _restartGameAction?.Invoke();
      _windowService.CloseWindow(WindowId);
    }
  }
}
