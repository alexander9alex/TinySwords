using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Level.Data;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Scenes.Data;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingCutSceneState : SimpleState
  {
    private readonly ICurtain _curtain;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;

    public LoadingCutSceneState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine)
    {
      _curtain = curtain;
      _sceneLoader = sceneLoader;
      _gameStateMachine = gameStateMachine;
    }

    public override void Enter() =>
      _curtain.Show(() => _sceneLoader.LoadScene(SceneId.CutScene, OnLoaded));

    private void OnLoaded() =>
      _gameStateMachine.Enter<CutSceneState>();
  }
}
