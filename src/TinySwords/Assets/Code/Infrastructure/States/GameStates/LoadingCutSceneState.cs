using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.CutScene;
using Code.Gameplay.CutScene.Data;
using Code.Gameplay.CutScene.Factory;
using Code.Gameplay.CutScene.Windows;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Scenes.Data;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingCutSceneState : SimpleState
  {
    private readonly ICurtain _curtain;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ICutSceneFactory _cutSceneFactory;
    private readonly ICameraProvider _cameraProvider;

    public LoadingCutSceneState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, ICutSceneFactory cutSceneFactory,
      ICameraProvider cameraProvider)
    {
      _curtain = curtain;
      _sceneLoader = sceneLoader;
      _gameStateMachine = gameStateMachine;
      _cutSceneFactory = cutSceneFactory;
      _cameraProvider = cameraProvider;
    }

    public override void Enter() =>
      _curtain.Show(() => _sceneLoader.LoadScene(SceneId.CutScene, OnLoaded));

    private void OnLoaded()
    {
      _cameraProvider.SetMainCamera(Camera.main);

      CutSceneWindow cutSceneWindow = _cutSceneFactory.CreateCutScene(CutSceneId.First);
      _gameStateMachine.Enter<CutSceneState, CutSceneWindow>(cutSceneWindow);
    }
  }
}
