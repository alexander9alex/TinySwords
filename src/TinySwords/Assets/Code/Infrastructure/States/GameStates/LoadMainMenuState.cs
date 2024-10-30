using Code.Gameplay.Common.Curtain;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadMainMenuState : SimpleState
  {
    private readonly ICurtain _curtain;

    public LoadMainMenuState(ICurtain curtain) =>
      _curtain = curtain;

    public override void Enter() =>
      _curtain.Show(OnShowed);

    private void OnShowed() =>
      _curtain.Hide();
  }
}
