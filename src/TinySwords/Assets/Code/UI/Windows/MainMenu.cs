using Code.Gameplay.CutScene.Data;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows
{
  public class MainMenu : MonoBehaviour
  {
    public Button PlayButton;
    public Button ExitGameButton;
    
    private IGameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine) =>
      _gameStateMachine = gameStateMachine;

    private void Awake()
    {
      PlayButton.onClick.AddListener(StartGame);
      ExitGameButton.onClick.AddListener(ExitGame);
    }

    private void StartGame() =>
      _gameStateMachine.Enter<LoadingCutSceneState, CutSceneId>(CutSceneId.First);

    private void ExitGame() =>
      Application.Quit();
  }
}
