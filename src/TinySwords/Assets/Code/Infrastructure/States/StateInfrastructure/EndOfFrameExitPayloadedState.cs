using RSG;

namespace Code.Infrastructure.States.StateInfrastructure
{
  public class EndOfFrameExitPayloadedState<TPayload> : IPayloadState<TPayload>, IUpdatable
  {
    private bool ExitWasRequested => _exitPromise != null;
    private Promise _exitPromise;
    public virtual void Enter(TPayload payload) {}
    protected virtual void Update() {}
    protected virtual void ExitOnEndOfFrame() {}

    void IUpdatable.Update()
    {
      if (!ExitWasRequested)
        Update();

      if (ExitWasRequested)
        ResolveExitPromise();
    }

    IPromise IExitableState.BeginExit() =>
      _exitPromise = new Promise();

    void IExitableState.EndExit()
    {
      ExitOnEndOfFrame();
      ClearExitPromise();
    }

    private void ResolveExitPromise() =>
      _exitPromise.Resolve();

    private void ClearExitPromise() =>
      _exitPromise = null;
  }
}
