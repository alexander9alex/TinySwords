using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Lose.Windows;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Level.Data;
using Code.Gameplay.Services;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.UI.Data;
using Code.UI.Windows.Services;

namespace Code.Gameplay.Features.Lose.Services
{
  public class LoseService : ILoseService
  {
    private readonly IWindowService _windowService;
    private readonly ITimeService _timeService;
    private readonly IInputService _inputService;
    private readonly ISoundService _soundService;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ICurtain _curtain;

    public LoseService(IWindowService windowService, ITimeService timeService, IInputService inputService, ISoundService soundService,
      IGameStateMachine gameStateMachine, ICurtain curtain)
    {
      _timeService = timeService;
      _windowService = windowService;
      _inputService = inputService;
      _soundService = soundService;
      _gameStateMachine = gameStateMachine;
      _curtain = curtain;
    }

    public void Lose()
    {
      _timeService.StopTime();
      _inputService.ChangeInputMap(InputMap.UI);
      _soundService.PlaySound(SoundId.ShowWindow);

      LoseWindow window = _windowService.OpenWindow<LoseWindow>(WindowId.LoseWindow);
      window.SetRestartLevelAction(RestartLevel);
    }

    private void RestartLevel()
    {
      _soundService.PlaySound(SoundId.HideWindow);
      _curtain.Show(() => LoadLevel(LevelId.First));
    }

    private void LoadLevel(LevelId levelId) =>
      _gameStateMachine.Enter<LoadingLevelState, LevelId>(levelId);
  }
}
