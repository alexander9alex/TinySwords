using Code.Gameplay;
using Code.Gameplay.Common.Curtain;
using Code.Infrastructure.Factory;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
  public class GameLoopState : EndOfFrameExitState
  {
    private readonly ICurtain _curtain;
    private readonly ISystemFactory _systemFactory;
    private readonly GameContext _gameContext;

    private GameplayFeature _gameplayFeature;

    public GameLoopState(ICurtain curtain, ISystemFactory systemFactory, GameContext gameContext)
    {
      _curtain = curtain;
      _systemFactory = systemFactory;
      _gameContext = gameContext;
    }

    public override void Enter()
    {
      _gameplayFeature = _systemFactory.Create<GameplayFeature>();
      _gameplayFeature.Initialize();
      
      _curtain.Hide();
    }

    protected override void Update()
    {
      _gameplayFeature.Execute();
      _gameplayFeature.Cleanup();
    }

    protected override void ExitOnEndOfFrame()
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
