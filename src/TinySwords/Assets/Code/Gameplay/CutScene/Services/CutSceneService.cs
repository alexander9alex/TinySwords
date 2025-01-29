using System.Collections;
using System.Collections.Generic;
using System.Text;
using Code.Gameplay.CutScene.Configs;
using Code.Gameplay.CutScene.Windows;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Factory;
using Code.Gameplay.Services;
using Code.Infrastructure.Common.CoroutineRunner;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Gameplay.CutScene.Services
{
  class CutSceneService : ICutSceneService
  {
    private const int Space = ' ';
    private readonly List<char> _completeSentenceMarks = new() { '.', '!', '?' };
    private readonly List<char> _marks = new() { '.', '!', '?', '-', ',', '\'', '"' };

    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ITimeService _time;
    private readonly ISoundFactory _soundFactory;

    private bool _click;

    public CutSceneService(ICoroutineRunner coroutineRunner, IGameStateMachine gameStateMachine, ITimeService time, ISoundFactory soundFactory)
    {
      _coroutineRunner = coroutineRunner;
      _gameStateMachine = gameStateMachine;
      _time = time;
      _soundFactory = soundFactory;
    }

    public void RunCutScene(CutSceneWindow cutSceneWindow) =>
      _coroutineRunner.StartCoroutine(RunCutSceneCoroutine(cutSceneWindow));

    private IEnumerator RunCutSceneCoroutine(CutSceneWindow cutSceneWindow)
    {
      cutSceneWindow.SetNextReplicaAction(NextReplica);
      CutSceneConfig config = cutSceneWindow.GetCutSceneConfig();
      GameEntity textDisplaySound = _soundFactory.CreateSound(SoundId.TextDisplay);

      foreach (string replica in config.Replicas)
      {
        cutSceneWindow.ClearReplica();
        yield return ShowReplica(cutSceneWindow, replica, config, textDisplaySound);
        yield return WaitToClick();
        yield return HideReplica(cutSceneWindow, config);
      }

      RemoveSound(textDisplaySound);

      _gameStateMachine.Enter<LoadingGameState>();
    }

    private IEnumerator ShowReplicaCoroutine(CutSceneWindow cutSceneWindow, string replica, CutSceneConfig config, GameEntity textDisplaySound)
    {
      ReplaySoundFirst(textDisplaySound);
      cutSceneWindow.SetReplicaFade(1);

      WaitForSeconds waitAfterSymbol = new(config.ReplicaSymbolDisplaySpeed);
      WaitForSeconds waitAfterEndSentence = new(config.ReplicaEndSentenceDisplaySpeed);

      StringBuilder displayReplicaPart = new();

      for (int i = 0; i < replica.Length; i++)
      {
        displayReplicaPart.Append(replica[i]);
        cutSceneWindow.SetReplica(displayReplicaPart.ToString());

        if (IsCompleteSentenceMark(replica, index: i))
        {
          PauseSound(textDisplaySound);
          yield return waitAfterEndSentence;
          ReplaySoundFirst(textDisplaySound);
        }

        if (IsMark(replica, index: i))
        {
          PauseSound(textDisplaySound);
          yield return waitAfterSymbol;
          ContinueSound(textDisplaySound);
        }
        else
          yield return waitAfterSymbol;

        if (_click)
        {
          cutSceneWindow.SetReplica(replica);
          PauseSound(textDisplaySound);
          yield break;
        }
      }

      PauseSound(textDisplaySound);
    }

    private IEnumerator HideReplica(CutSceneWindow cutSceneWindow, CutSceneConfig config)
    {
      float hideReplicaTimer = 0;

      while (hideReplicaTimer < config.TimeToHideReplica)
      {
        cutSceneWindow.SetReplicaFade(1 - hideReplicaTimer / config.TimeToHideReplica);
        hideReplicaTimer += _time.DeltaTime;
        yield return null;

        if (_click)
        {
          cutSceneWindow.SetReplicaFade(0);
          _click = false;
          yield break;
        }
      }

      cutSceneWindow.SetReplicaFade(0);
      float waitToNextReplicaTimer = 0;

      while (waitToNextReplicaTimer < config.TimeToWaitNextReplica)
      {
        waitToNextReplicaTimer += _time.DeltaTime;
        yield return null;

        if (_click)
        {
          _click = false;
          yield break;
        }
      }
    }

    private IEnumerator WaitToClick()
    {
      _click = false;

      while (!_click)
        yield return null;

      _click = false;
    }

    private bool IsCompleteSentenceMark(string replica, int index)
    {
      if (replica.Length > index + 1)
        return _completeSentenceMarks.Contains(replica[index]) && replica[index + 1] == Space;

      return true;
    }

    private bool IsMark(string replica, int index)
    {
      if (_marks.Contains(replica[index]))
        return true;

      if (index > 0 && _marks.Contains(replica[index - 1]) && replica[index] == Space)
        return true;

      if (replica.Length > index + 1 && _marks.Contains(replica[index + 1]) && replica[index] == Space)
        return true;

      return false;
    }

    private static void ReplaySoundFirst(GameEntity textDisplaySound)
    {
      textDisplaySound.isResetSoundPlaybackTimeRequest = true;
      ContinueSound(textDisplaySound);
    }

    private static void ContinueSound(GameEntity textDisplaySound)
    {
      textDisplaySound.isPlaySoundRequest = true;
      textDisplaySound.isPauseSoundRequest = false;
    }

    private static void PauseSound(GameEntity textDisplaySound)
    {
      textDisplaySound.isPauseSoundRequest = true;
      textDisplaySound.isPlaySoundRequest = false;
    }

    private void RemoveSound(GameEntity textDisplaySound) =>
      textDisplaySound.isDestructed = true;

    private void NextReplica() =>
      _click = true;

    private Coroutine ShowReplica(CutSceneWindow cutSceneWindow, string replica, CutSceneConfig config, GameEntity textDisplaySound) =>
      _coroutineRunner.StartCoroutine(ShowReplicaCoroutine(cutSceneWindow, replica, config, textDisplaySound));
  }
}
