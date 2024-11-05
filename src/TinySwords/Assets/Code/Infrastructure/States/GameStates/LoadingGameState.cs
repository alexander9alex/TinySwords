using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.Build.Data;
using Code.Gameplay.Features.Build.Factory;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Units.Data;
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
    private readonly ICameraProvider _cameraProvider;
    private readonly IInputService _inputService;
    private readonly IBuildingFactory _buildingFactory;

    public LoadingGameState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, ICameraProvider cameraProvider,
      IInputService inputService, IBuildingFactory buildingFactory)
    {
      _curtain = curtain;
      _sceneLoader = sceneLoader;
      _gameStateMachine = gameStateMachine;
      _cameraProvider = cameraProvider;
      _inputService = inputService;
      _buildingFactory = buildingFactory;
    }

    public override void Enter() =>
      _curtain.Show(() => _sceneLoader.LoadScene(GameScene, OnLoaded));

    private void OnLoaded()
    {
      _cameraProvider.SetMainCamera(Camera.main);
      _inputService.ChangeInputMap(InputMap.Game);

      _buildingFactory.CreateBuilding(BuildingTypeId.Castle, TeamColor.Blue, new Vector3(0, 0));

      _gameStateMachine.Enter<GameLoopState>();
    }
  }
}
