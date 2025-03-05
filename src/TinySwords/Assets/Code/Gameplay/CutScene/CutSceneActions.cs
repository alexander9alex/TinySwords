using System;
using System.Collections.Generic;
using Code.Gameplay.CutScene.Data;
using Code.Gameplay.Level.Data;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;

namespace Code.Gameplay.CutScene
{
  public class CutSceneActions
  {
    public readonly Dictionary<CutSceneId, Action> EndActions;
    private readonly IGameStateMachine _gameStateMachine;

    public CutSceneActions(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;

      EndActions = new()
      {
        { CutSceneId.First, LoadFirstLevel },
        { CutSceneId.Second, LoadMainMenu },
      };
    }

    private void LoadFirstLevel() =>
      _gameStateMachine.Enter<LoadingLevelState, LevelId>(LevelId.First);

    private void LoadMainMenu() =>
      _gameStateMachine.Enter<LoadingMainMenuState>();
  }
}
