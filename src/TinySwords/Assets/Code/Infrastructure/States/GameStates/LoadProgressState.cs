using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadProgressState : SimpleState
  {
    private readonly IGameStateMachine _gameStateMachine;

    public LoadProgressState(IGameStateMachine gameStateMachine) =>
      _gameStateMachine = gameStateMachine;

    public override void Enter() =>
      _gameStateMachine.Enter<LoadingMainMenuState>();
  }
}
