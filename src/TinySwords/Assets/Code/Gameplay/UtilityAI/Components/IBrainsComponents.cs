namespace Code.Gameplay.UtilityAI.Components
{
  public interface IBrainsComponents
  {
    When When { get; }
    GetInput GetInput { get; }
    Score Score { get; }
  }
}
