using System;
using System.Collections;
using Code.Infrastructure.Common.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
  class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) =>
      _coroutineRunner = coroutineRunner;

    public void LoadScene(string scene, Action onLoaded = null) =>
      _coroutineRunner.StartCoroutine(LoadSceneCoroutine(scene, onLoaded));

    private IEnumerator LoadSceneCoroutine(string scene, Action onLoaded)
    {
      if (SceneManager.GetActiveScene().name == scene)
      {
        onLoaded?.Invoke();
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(scene);

      while (!waitNextScene.isDone)
        yield return null;
      
      onLoaded?.Invoke();
    }
  }
}
