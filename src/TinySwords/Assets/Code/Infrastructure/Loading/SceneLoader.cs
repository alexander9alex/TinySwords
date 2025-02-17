using System;
using System.Collections;
using Code.Gameplay.Common.Services;
using Code.Infrastructure.Common.CoroutineRunner;
using Code.Infrastructure.Scenes.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
  class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IStaticDataService _staticData;

    public SceneLoader(ICoroutineRunner coroutineRunner, IStaticDataService staticData)
    {
      _coroutineRunner = coroutineRunner;
      _staticData = staticData;
    }

    public void LoadScene(SceneId sceneId, Action onLoaded = null)
    {
      string scene = _staticData.GetSceneNameById(sceneId);

      if (SceneManager.GetActiveScene().name == scene)
        onLoaded?.Invoke();
      else
        _coroutineRunner.StartCoroutine(LoadSceneCoroutine(_staticData.GetSceneNameById(sceneId), onLoaded));
    }

    public void ReloadScene(SceneId sceneId, Action onLoaded = null) =>
      _coroutineRunner.StartCoroutine(LoadSceneCoroutine(_staticData.GetSceneNameById(sceneId), onLoaded));

    private static IEnumerator LoadSceneCoroutine(string scene, Action onLoaded)
    {
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(scene);

      while (!waitNextScene.isDone)
        yield return null;

      onLoaded?.Invoke();
    }
  }
}
