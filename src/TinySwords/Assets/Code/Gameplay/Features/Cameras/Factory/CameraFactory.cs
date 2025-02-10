using Code.Gameplay.Common.Services;
using Code.Gameplay.Level.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.Cameras.Factory
{
  public class CameraFactory : ICameraFactory
  {
    private readonly IStaticDataService _staticData;

    public CameraFactory(IStaticDataService staticData)
    {
      _staticData = staticData;
    }

    public void CreateCamera(LevelConfig levelConfig)
    {
      Camera camera = Object.Instantiate(_staticData.GetCameraConfig().CameraPrefab).GetComponent<Camera>();
      camera.transform.position = levelConfig.CameraSpawnMarker.transform.position;
      camera.orthographicSize = levelConfig.CameraSpawnMarker.Size;
    }
  }
}
