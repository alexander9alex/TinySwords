using Code.Gameplay.Common.Curtain;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingMainMenuState : SimpleState
  {
    private const string MainMenuScene = "MainMenu";
    
    private readonly ICurtain _curtain;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;

    public LoadingMainMenuState(ICurtain curtain, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine)
    {
      _curtain = curtain;
      _sceneLoader = sceneLoader;
      _gameStateMachine = gameStateMachine;
    }

    public override void Enter() =>
      _curtain.Show(() => _sceneLoader.LoadScene(MainMenuScene, OnLoaded));

    private void OnLoaded() =>
      _gameStateMachine.Enter<MainMenuState>();
  }
}
