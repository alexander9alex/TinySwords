using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.StateInfrastructure;
using RSG;

namespace Code.Infrastructure.States.StateMachine
{
  public class GameStateMachine : IGameStateMachine
  {
    private IStateFactory _stateFactory;
    private IExitableState _activeState;

    public void Enter<TState>() where TState : class, IState =>
      RequestEnter<TState>()
        .Done();

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> =>
      RequestEnter<TState, TPayload>(payload)
        .Done();

    private IPromise<TState> RequestEnter<TState>() where TState : class, IState =>
      RequestChangeState<TState>()
        .Then(EnterState);

    private IPromise<TState> RequestEnter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> =>
      RequestChangeState<TState>()
        .Then(state => EnterPayloadState(state, payload));

    private IPromise<TState> RequestChangeState<TState>() where TState : class, IExitableState
    {
      if (_activeState != null)
      {
        _activeState.BeginExit()
          .Then(_activeState.EndExit)
          .Then(GetState<TState>);
      }

      return GetState<TState>();
    }

    private TState EnterState<TState>(TState state) where TState : class, IState
    {
      _activeState = state;
      state.Enter();
      return state;
    }

    private TState EnterPayloadState<TState, TPayload>(TState state, TPayload payload) where TState : class, IPayloadState<TPayload>
    {
      _activeState = state;
      state.Enter(payload);
      return state;
    }

    private IPromise<TState> GetState<TState>() where TState : class, IExitableState
    {
      TState state = _stateFactory.GetState<TState>();

      return Promise<TState>.Resolved(state);
    }
  }
}
