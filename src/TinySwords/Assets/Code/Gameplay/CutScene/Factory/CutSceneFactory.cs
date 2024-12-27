using Code.Gameplay.Common.Services;
using Code.Gameplay.CutScene.Configs;
using Code.Gameplay.CutScene.Data;
using Code.Gameplay.CutScene.Windows;
using UnityEngine;

namespace Code.Gameplay.CutScene.Factory
{
  public class CutSceneFactory : ICutSceneFactory
  {
    private readonly IStaticDataService _staticData;

    public CutSceneFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public CutSceneWindow CreateCutScene(CutSceneId cutSceneId)
    {
      CutSceneConfig config = _staticData.GetCutSceneConfig(cutSceneId);
      
      CutSceneWindow cutSceneWindow = Object.Instantiate(config.CutScenePrefab).GetComponent<CutSceneWindow>();
      cutSceneWindow.Construct(config);
      
      return cutSceneWindow;
    }
  }
}
