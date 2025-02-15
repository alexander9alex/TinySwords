using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Lose.Windows;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Services;
using Code.UI.Data;
using Code.UI.Windows.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Lose.Services
{
  public class LoseService : ILoseService
  {
    private readonly IWindowService _windowService;
    private readonly ITimeService _timeService;
    private readonly IInputService _inputService;
    private readonly ISoundService _soundService;

    public LoseService(IWindowService windowService, ITimeService timeService, IInputService inputService, ISoundService soundService)
    {
      _timeService = timeService;
      _windowService = windowService;
      _inputService = inputService;
      _soundService = soundService;
    }

    public void Lose()
    {
      _timeService.FreezeTime();
      _inputService.ChangeInputMap(InputMap.UI);
      _soundService.PlaySound(SoundId.ShowWindow);

      LoseWindow window = _windowService.OpenWindow<LoseWindow>(WindowId.LoseWindow);
      window.SetRestartAction(RestartGame);
    }

    private void RestartGame()
    {
      _soundService.PlaySound(SoundId.HideWindow);

      Debug.Log("Game restarted!");
    }
  }
}
