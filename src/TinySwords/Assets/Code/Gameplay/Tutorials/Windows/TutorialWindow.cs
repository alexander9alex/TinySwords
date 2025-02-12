using Code.Gameplay.Tutorials.Data;
using Code.Gameplay.Tutorials.Extensions;
using Code.UI.Windows;
using Code.UI.Windows.Services;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Tutorials.Windows
{
  public class TutorialWindow : BaseWindow
  {
    public TutorialId TutorialId;

    public Button NextPageButton;
    public Button PreviousPageButton;
    public Button CompleteTutorialButton;
    
    private IWindowService _windowService;

    [Inject]
    private void Construct(IWindowService windowService)
    {
      WindowId = TutorialId.ToWindowId();
      
      _windowService = windowService;
    }

    protected override void Initialize()
    {
      NextPageButton.onClick.AddListener(NextPage);
      PreviousPageButton.onClick.AddListener(PreviousPage);
      CompleteTutorialButton.onClick.AddListener(CompleteTutorial);
    }

    private void NextPage()
    {
      
    }

    private void PreviousPage()
    {
      
    }

    private void CompleteTutorial() =>
      _windowService.CloseWindow(WindowId);
  }
}
