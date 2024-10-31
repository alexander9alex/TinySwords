using Code.Gameplay.Services;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class BootstrapState : SimpleState
  {
    private const int TargetFrameRate = 60;

    private readonly IGameStateMachine _gameStateMachine;
    private readonly IStaticDataService _staticDataService;

    public BootstrapState(IGameStateMachine gameStateMachine, IStaticDataService staticDataService)
    {
      _gameStateMachine = gameStateMachine;
      _staticDataService = staticDataService;
    }

    public override void Enter()
    {
      Application.targetFrameRate = TargetFrameRate;

      _staticDataService.LoadAll();
      
      _gameStateMachine.Enter<LoadProgressState>();
    }
  }
}
