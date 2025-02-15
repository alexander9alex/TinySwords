using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Features.Win.Windows;
using Code.Gameplay.Services;
using Code.UI.Data;
using Code.UI.Windows.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Win.Services
{
  class WinService : IWinService
  {
    private readonly ITimeService _timeService;
    private readonly IWindowService _windowService;
    private readonly IInputService _inputService;
    private readonly ISoundService _soundService;

    public WinService(ITimeService timeService, IWindowService windowService, IInputService inputService, ISoundService soundService)
    {
      _timeService = timeService;
      _windowService = windowService;
      _inputService = inputService;
      _soundService = soundService;
    }

    public void Win()
    {
      _timeService.FreezeTime();
      _inputService.ChangeInputMap(InputMap.UI);
      _soundService.PlaySound(SoundId.ShowWindow);

      WinWindow window = _windowService.OpenWindow<WinWindow>(WindowId.WinWindow);
      window.Construct();
      window.SetContinueGameAction(ContinueGame);
    }

    private void ContinueGame()
    {
      _soundService.PlaySound(SoundId.HideWindow);
      _windowService.CloseWindow(WindowId.WinWindow);
      
      Debug.Log("Game continues!");
    }
  }
}
