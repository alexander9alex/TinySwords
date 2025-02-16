using Code.Gameplay.Common.Services;
using Code.Gameplay.Level.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Factory
{
  public class CameraFactory : ICameraFactory
  {
    private readonly GameObject _cameraPrefab;

    public CameraFactory(IStaticDataService staticData) =>
      _cameraPrefab = staticData.GetCameraConfig().CameraPrefab;

    public void CreateCamera(LevelConfig levelConfig)
    {
      Camera camera = Object.Instantiate(_cameraPrefab).GetComponent<Camera>();
      camera.transform.position = levelConfig.CameraSpawnMarker.transform.position;
      camera.orthographicSize = levelConfig.CameraSpawnMarker.Size;
    }
  }
}
