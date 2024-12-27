using Code.Gameplay.CutScene.Data;
using Code.Gameplay.CutScene.Windows;

namespace Code.Gameplay.CutScene.Factory
{
  public interface ICutSceneFactory
  {
    CutSceneWindow CreateCutScene(CutSceneId cutSceneId);
  }
}