using System;
using Code.UI.Data;
using Code.UI.Windows;
using Code.UI.Windows.Services;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Tutorials.Windows
{
  public class WantToCompleteTutorialBaseWindow : BaseWindow
  {
    public Button PositiveAnswerButton;
    public Button NegativeAnswerButton;
    
    private IWindowService _windowService;
    private Action _positiveAnswerAction;
    private Action _negativeAnswerAction;

    [Inject]
    private void Construct(IWindowService windowService)
    {
      WindowId = WindowId.WantToCompleteTutorial;

      _windowService = windowService;
    }

    protected override void Initialize()
    {
      PositiveAnswerButton.onClick.AddListener(PositiveAnswer);
      NegativeAnswerButton.onClick.AddListener(NegativeAnswer);
    }

    public void SetPositiveAnswerAction(Action action) =>
      _positiveAnswerAction = action;

    public void SetNegativeAnswerAction(Action action) =>
      _negativeAnswerAction = action;

    private void PositiveAnswer()
    {
      _positiveAnswerAction?.Invoke();
      _windowService.CloseWindow(WindowId);
    }

    private void NegativeAnswer()
    {
      _negativeAnswerAction?.Invoke();
      _windowService.CloseWindow(WindowId);
    }
  }
}
