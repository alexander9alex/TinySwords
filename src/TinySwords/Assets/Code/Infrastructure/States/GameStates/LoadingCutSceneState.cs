using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.CutScene.Data;
using Code.Gameplay.CutScene.Factory;
using Code.Gameplay.CutScene.Windows;
using Code.Gameplay.Services;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Scenes.Data;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingCutSceneState : SimplePayloadedState<CutSceneId>
  {
    private readonly ICurtain _curtain;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ICutSceneFactory _cutSceneFactory;
    private readonly ICameraProvider _cameraProvider;
    private readonly ITimeService _timeService;

    public LoadingCutSceneState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, ICutSceneFactory cutSceneFactory,
      ICameraProvider cameraProvider, ITimeService timeService)
    {
      _curtain = curtain;
      _sceneLoader = sceneLoader;
      _gameStateMachine = gameStateMachine;
      _cutSceneFactory = cutSceneFactory;
      _cameraProvider = cameraProvider;
      _timeService = timeService;
    }

    public override void Enter(CutSceneId cutSceneId) =>
      _curtain.Show(() => _sceneLoader.LoadScene(SceneId.CutScene, () => OnLoaded(cutSceneId)));

    private void OnLoaded(CutSceneId cutSceneId)
    {
      _cameraProvider.SetMainCamera(Camera.main);
      _timeService.StartTime();

      CutSceneWindow cutSceneWindow = _cutSceneFactory.CreateCutScene(cutSceneId);
      _gameStateMachine.Enter<CutSceneState, CutSceneWindow>(cutSceneWindow);
    }
  }
}
