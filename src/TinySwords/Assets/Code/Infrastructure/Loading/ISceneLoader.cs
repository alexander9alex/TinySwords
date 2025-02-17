using System;
using Code.Infrastructure.Scenes.Data;

namespace Code.Infrastructure.Loading
{
  public interface ISceneLoader
  {
    void LoadScene(SceneId sceneId, Action onLoaded = null);
    void ReloadScene(SceneId sceneId, Action onLoaded = null);
  }
}
