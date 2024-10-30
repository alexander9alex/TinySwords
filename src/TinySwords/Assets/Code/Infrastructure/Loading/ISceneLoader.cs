using System;

namespace Code.Infrastructure.Loading
{
  public interface ISceneLoader
  {
    void LoadScene(string scene, Action onLoaded = null);
  }
}
