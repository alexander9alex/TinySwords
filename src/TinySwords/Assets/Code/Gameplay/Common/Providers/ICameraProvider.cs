using UnityEngine;

namespace Code.Gameplay.Common.Providers
{
  public interface ICameraProvider
  {
    Camera MainCamera { get; }
    void SetMainCamera(Camera camera);
  }
}
