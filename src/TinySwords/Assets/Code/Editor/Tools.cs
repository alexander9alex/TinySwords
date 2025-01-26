using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Code.Editor
{
  public class Tools
  {
    [MenuItem("Tools/Start Game Without Cutscene")]
    public static void StartGameWithoutCutscene() =>
      Object.FindFirstObjectByType<ProjectContext>().Container.Resolve<IGameStateMachine>().Enter<LoadingGameState>();
  }
}
