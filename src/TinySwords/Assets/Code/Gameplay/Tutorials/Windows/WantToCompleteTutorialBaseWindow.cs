using System;
using Code.UI.Data;
using Code.UI.Windows;
using Code.UI.Windows.Services;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Tutorials.Windows
{
  public class WantToCompleteTutorialBaseWindow : BaseWindow
  {
    public Button PositiveAnswerButton;
    public Button NegativeAnswerButton;
    
    private IWindowService _windowService;
    private Action _positiveAnswer;

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

    public void SetPositiveAction(Action positiveAnswer) =>
      _positiveAnswer = positiveAnswer;

    private void PositiveAnswer()
    {
      _positiveAnswer?.Invoke();
      _windowService.CloseWindow(WindowId);
    }

    private void NegativeAnswer() =>
      _windowService.CloseWindow(WindowId);
  }
}
