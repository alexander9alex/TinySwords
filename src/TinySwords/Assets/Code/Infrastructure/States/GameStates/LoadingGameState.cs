using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Features.Units.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingGameState : SimpleState
  {
    private const string GameScene = "Game";

    private readonly ICurtain _curtain;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IUnitFactory _unitFactory;
    private readonly ICameraProvider _cameraProvider;
    private readonly IInputService _inputService;

    public LoadingGameState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IUnitFactory unitFactory,
      ICameraProvider cameraProvider, IInputService inputService)
    {
      _curtain = curtain;
      _sceneLoader = sceneLoader;
      _gameStateMachine = gameStateMachine;
      _unitFactory = unitFactory;
      _cameraProvider = cameraProvider;
      _inputService = inputService;
    }

    public override void Enter() =>
      _curtain.Show(() => _sceneLoader.LoadScene(GameScene, OnLoaded));

    private void OnLoaded()
    {
      _cameraProvider.SetMainCamera(Camera.main);
      _inputService.ChangeInputMap(InputMap.Game);
        
      _unitFactory.CreateUnit(UnitTypeId.Knight, UnitColor.Blue, new Vector3(0, 0));

      _gameStateMachine.Enter<GameLoopState>();
    }
  }
}
