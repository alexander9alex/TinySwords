using Code.Gameplay;
using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Tutorials.Data;
using Code.Gameplay.Tutorials.Services;
using Code.Infrastructure.Factory;
using Code.Infrastructure.States.StateInfrastructure;
using Code.UI.Windows.Services;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelState : EndOfFrameExitState
  {
    private readonly ICurtain _curtain;
    private readonly ISystemFactory _systemFactory;
    private readonly GameContext _gameContext;
    private readonly ITutorialService _tutorialService;
    private readonly IWindowService _windowService;

    private GameplayFeature _gameplayFeature;
    private readonly IInputService _inputService;

    public LevelState(ICurtain curtain, ISystemFactory systemFactory, GameContext gameContext, ITutorialService tutorialService, IWindowService windowService,
      IInputService inputService)
    {
      _curtain = curtain;
      _systemFactory = systemFactory;
      _gameContext = gameContext;
      _tutorialService = tutorialService;
      _windowService = windowService;
      _inputService = inputService;
    }

    public override void Enter()
    {
      _gameplayFeature = _systemFactory.Create<GameplayFeature>();
      _gameplayFeature.Initialize();

      _curtain.Hide(OnEnded);
    }

    private void OnEnded() =>
      _tutorialService.ShowTutorial(TutorialId.First);

    protected override void Update()
    {
      _gameplayFeature.Execute();
      _gameplayFeature.Cleanup();
    }

    protected override void ExitOnEndOfFrame() =>
      _curtain.Show(CleanupLevel);

    private void CleanupLevel()
    {
      CleanupGameplayFeature();
      _windowService.Cleanup();
      _inputService.Cleanup();
    }

    private void CleanupGameplayFeature()
    {
      _gameplayFeature.ClearReactiveSystems();
      _gameplayFeature.DeactivateReactiveSystems();

      DestructEntities();

      _gameplayFeature.Cleanup();
      _gameplayFeature.TearDown();
      _gameplayFeature = null;
    }

    private void DestructEntities()
    {
      foreach (GameEntity entity in _gameContext.GetEntities())
        entity.isDestructed = true;
    }
  }
}
