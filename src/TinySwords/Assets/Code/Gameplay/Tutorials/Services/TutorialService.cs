using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Services;
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
    private readonly ITimeService _timeService;
    private readonly IInputService _inputService;
    private readonly ISoundService _soundService;

    public TutorialService(IWindowService windowService, ITimeService timeService, IInputService inputService, ISoundService soundService)
    {
      _windowService = windowService;
      _timeService = timeService;
      _inputService = inputService;
      _soundService = soundService;
    }

    public void ShowTutorial(TutorialId tutorialId)
    {
      _inputService.ChangeInputMap(InputMap.UI);
      _timeService.FreezeTime();

      BaseWindow baseWindow = _windowService.OpenWindow(WindowId.WantToCompleteTutorial);
      _soundService.PlaySound(SoundId.ShowWindow);

      if (baseWindow is WantToCompleteTutorialBaseWindow wantToCompleteTutorialWindow)
      {
        wantToCompleteTutorialWindow.SetPositiveAction(() => CreateTutorialWindow(tutorialId));
        wantToCompleteTutorialWindow.SetNegativeAction(CancelTutorial);
      }
    }

    private void CancelTutorial()
    {
      _soundService.PlaySound(SoundId.HideWindow);
      _inputService.ChangeInputMap(InputMap.Game);
      _timeService.UnfreezeTime();
    }

    private void CreateTutorialWindow(TutorialId tutorialId) =>
      _windowService.OpenWindow(tutorialId.ToWindowId());
  }
}
