namespace Code.Gameplay.UtilityAI
{
  public interface IBrainsComponents
  {
    When When { get; }
    GetInput GetInput { get; }
    Score Score { get; }
  }
}
