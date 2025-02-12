using Code.Gameplay.Tutorials.Data;
using Code.Gameplay.Tutorials.Extensions;
using Code.Gameplay.Tutorials.Windows;
using Code.UI.Data;
using Code.UI.Windows;
using Code.UI.Windows.Services;

namespace Code.Gameplay.Tutorials.Services
{
  class TutorialService : ITutorialService
  {
    private readonly IWindowService _windowService;

    public TutorialService(IWindowService windowService) =>
      _windowService = windowService;

    public void ShowTutorial(TutorialId tutorialId)
    {
      BaseWindow baseWindow = _windowService.OpenWindow(WindowId.WantToCompleteTutorial);
      
      if (baseWindow is WantToCompleteTutorialBaseWindow wantToCompleteTutorialWindow)
        wantToCompleteTutorialWindow.SetPositiveAction(() => CreateTutorialWindow(tutorialId));
    }

    private void CreateTutorialWindow(TutorialId tutorialId) =>
      _windowService.OpenWindow(tutorialId.ToWindowId());
  }
}
