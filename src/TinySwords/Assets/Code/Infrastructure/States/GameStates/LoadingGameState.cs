using Code.Gameplay.Common.Curtain;
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

    public LoadingGameState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IUnitFactory unitFactory)
    {
      _curtain = curtain;
      _sceneLoader = sceneLoader;
      _gameStateMachine = gameStateMachine;
      _unitFactory = unitFactory;
    }

    public override void Enter() =>
      _curtain.Show(() => _sceneLoader.LoadScene(GameScene, OnLoaded));

    private void OnLoaded()
    {
      _unitFactory.CreateUnit(UnitTypeId.Knight, UnitColor.Blue, new Vector3(0, 0));
      
      _gameStateMachine.Enter<GameLoopState>();
    }
  }
}
