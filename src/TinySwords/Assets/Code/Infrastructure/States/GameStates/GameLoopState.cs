using Code.Gameplay.Common.Curtain;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
  public class GameLoopState : EndOfFrameExitState
  {
    private readonly ICurtain _curtain;

    public GameLoopState(ICurtain curtain) =>
      _curtain = curtain;

    public override void Enter()
    {
      _curtain.Hide();
    }
  }
}
