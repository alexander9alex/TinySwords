using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class BootstrapState : SimpleState
  {
    private const int TargetFrameRate = 60;

    private readonly IGameStateMachine _gameStateMachine;

    public BootstrapState(IGameStateMachine gameStateMachine) =>
      _gameStateMachine = gameStateMachine;

    public override void Enter()
    {
      Application.targetFrameRate = TargetFrameRate;

      _gameStateMachine.Enter<LoadProgressState>();
    }
  }
}
