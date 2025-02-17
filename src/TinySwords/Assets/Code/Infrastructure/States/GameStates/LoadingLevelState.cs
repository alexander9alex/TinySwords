using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Cameras.Factory;
using Code.Gameplay.Features.Cameras.Services;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Data;
using Code.Gameplay.Level.Factory;
using Code.Gameplay.Services;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Scenes.Data;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingLevelState : SimplePayloadedState<LevelId>
  {
    private readonly ICurtain _curtain;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ICameraProvider _cameraProvider;
    private readonly IInputService _inputService;
    private readonly ILevelFactory _levelFactory;
    private readonly ICameraMovementService _cameraMovementService;
    private readonly ICameraFactory _cameraFactory;
    private readonly IStaticDataService _staticData;
    private readonly ITimeService _timeService;

    public LoadingLevelState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, ICameraProvider cameraProvider,
      IInputService inputService, ILevelFactory levelFactory, ICameraMovementService cameraMovementService, ICameraFactory cameraFactory,
      IStaticDataService staticData, ITimeService timeService)
    {
      _curtain = curtain;
      _sceneLoader = sceneLoader;
      _gameStateMachine = gameStateMachine;
      _cameraProvider = cameraProvider;
      _inputService = inputService;
      _levelFactory = levelFactory;
      _cameraMovementService = cameraMovementService;
      _cameraFactory = cameraFactory;
      _staticData = staticData;
      _timeService = timeService;
    }

    public override void Enter(LevelId levelId) =>
      _curtain.Show(() => _sceneLoader.ReloadScene(SceneId.Game, () => OnLoaded(levelId)));

    private void OnLoaded(LevelId levelId)
    {
      LevelConfig config = _staticData.GetLevelConfig(levelId);

      _cameraFactory.CreateCamera(config);
      _cameraProvider.SetMainCamera(Camera.main);

      _levelFactory.CreateLevel(config);
      _cameraMovementService.SetCameraBorders(config);

      _inputService.ChangeInputMap(InputMap.Game);
      _timeService.UnfreezeTime();

      _gameStateMachine.Enter<LevelState>();
    }
  }
}
