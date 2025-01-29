using Code.Gameplay;
using Code.Gameplay.Common.Curtain;
using Code.Gameplay.CutScene.Services;
using Code.Gameplay.CutScene.Windows;
using Code.Infrastructure.Factory;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
  public class CutSceneState : EndOfFrameExitPayloadedState<CutSceneWindow>
  {
    private readonly ICurtain _curtain;
    private readonly ICutSceneService _cutSceneService;
    private readonly ISystemFactory _systemFactory;
    private readonly GameContext _gameContext;
    
    private CutSceneFeature _cutSceneFeature;

    public CutSceneState(ICurtain curtain, ICutSceneService cutSceneService, ISystemFactory systemFactory, GameContext gameContext)
    {
      _curtain = curtain;
      _cutSceneService = cutSceneService;
      _systemFactory = systemFactory;
      _gameContext = gameContext;
    }

    public override void Enter(CutSceneWindow cutSceneWindow)
    {
      _cutSceneFeature = _systemFactory.Create<CutSceneFeature>();
      _cutSceneFeature.Initialize();
      
      _curtain.Hide(() => OnCurtainHided(cutSceneWindow));
    }

    protected override void Update()
    {
      _cutSceneFeature.Execute();
      _cutSceneFeature.Cleanup();
    }

    protected override void ExitOnEndOfFrame()
    {
      _cutSceneFeature.ClearReactiveSystems();
      _cutSceneFeature.DeactivateReactiveSystems();

      DestructEntities();
      
      _cutSceneFeature.Cleanup();
      _cutSceneFeature.TearDown();
      _cutSceneFeature = null;
    }

    private void OnCurtainHided(CutSceneWindow cutSceneWindow) =>
      _cutSceneService.RunCutScene(cutSceneWindow);

    private void DestructEntities()
    {
      foreach (GameEntity entity in _gameContext.GetEntities())
        entity.isDestructed = true;
    }
  }
}
