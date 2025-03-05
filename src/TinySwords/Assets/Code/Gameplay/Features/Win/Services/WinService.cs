using Code.Gameplay.CutScene.Data;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Features.Win.Windows;
using Code.Gameplay.Services;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.UI.Data;
using Code.UI.Windows.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Win.Services
{
  class WinService : IWinService
  {
    private readonly IWindowService _windowService;
    private readonly ITimeService _timeService;
    private readonly IInputService _inputService;
    private readonly ISoundService _soundService;
    private readonly IGameStateMachine _gameStateMachine;

    public WinService(IWindowService windowService, ITimeService timeService, IInputService inputService, ISoundService soundService,
      IGameStateMachine gameStateMachine)
    {
      _timeService = timeService;
      _windowService = windowService;
      _inputService = inputService;
      _soundService = soundService;
      _gameStateMachine = gameStateMachine;
    }

    public void Win()
    {
      _timeService.StopTime();
      _inputService.ChangeInputMap(InputMap.UI);
      _soundService.PlaySound(SoundId.ShowWindow);

      WinWindow window = _windowService.OpenWindow<WinWindow>(WindowId.WinWindow);
      window.SetContinueGameAction(ContinueGame);
    }

    private void ContinueGame()
    {
      _soundService.PlaySound(SoundId.HideWindow);
      _gameStateMachine.Enter<LoadingCutSceneState, CutSceneId>(CutSceneId.Second);
    }
  }
}
