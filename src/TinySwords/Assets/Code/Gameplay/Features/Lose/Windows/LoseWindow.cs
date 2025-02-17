using System;
using Code.UI.Data;
using Code.UI.Windows;
using Code.UI.Windows.Services;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.Lose.Windows
{
  public class LoseWindow : BaseWindow
  {
    public Button RestartLevelButton;

    private IWindowService _windowService;
    private Action _restartLevelAction;

    [Inject]
    private void Construct(IWindowService windowService)
    {
      WindowId = WindowId.LoseWindow;

      _windowService = windowService;
    }

    protected override void Initialize() =>
      RestartLevelButton.onClick.AddListener(RestartGame);

    public void SetRestartLevelAction(Action action) =>
      _restartLevelAction = action;

    private void RestartGame()
    {
      _restartLevelAction?.Invoke();
      _windowService.CloseWindow(WindowId);
    }
  }
}
