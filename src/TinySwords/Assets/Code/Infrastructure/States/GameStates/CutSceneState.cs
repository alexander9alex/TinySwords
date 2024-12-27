using Code.Gameplay.Common.Curtain;
using Code.Gameplay.CutScene.Services;
using Code.Gameplay.CutScene.Windows;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
  public class CutSceneState : SimplePayloadedState<CutSceneWindow>
  {
    private readonly ICurtain _curtain;
    private readonly ICutSceneService _cutSceneService;

    public CutSceneState(ICurtain curtain, ICutSceneService cutSceneService)
    {
      _curtain = curtain;
      _cutSceneService = cutSceneService;
    }

    public override void Enter(CutSceneWindow cutSceneWindow) =>
      _curtain.Hide(() => OnCurtainHided(cutSceneWindow));

    private void OnCurtainHided(CutSceneWindow cutSceneWindow) =>
      _cutSceneService.RunCutScene(cutSceneWindow);
  }
}
