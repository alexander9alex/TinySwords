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
using Code.Infrastructure.Loading;
using Code.Infrastructure.Scenes.Data;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingGameState : SimpleState
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

    public LoadingGameState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, ICameraProvider cameraProvider,
      IInputService inputService, ILevelFactory levelFactory, ICameraMovementService cameraMovementService, ICameraFactory cameraFactory,
      IStaticDataService staticData)
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
    }

    public override void Enter() =>
      _curtain.Show(() => _sceneLoader.LoadScene(SceneId.Game, OnLoaded));

    private void OnLoaded()
    {
      LevelConfig config = _staticData.GetLevelConfig(LevelId.First);

      _cameraFactory.CreateCamera(config);
      _cameraProvider.SetMainCamera(Camera.main);
      
      _levelFactory.CreateLevel(config);
      _cameraMovementService.SetCameraBorders(config);

      _inputService.ChangeInputMap(InputMap.Game);

      _gameStateMachine.Enter<GameLoopState>();
    }
  }
}
