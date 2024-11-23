namespace Code.Gameplay.UtilityAI.Components
{
  class BrainsComponents : IBrainsComponents
  {
    public When When { get; }
    public GetInput GetInput { get; }
    public Score Score { get; }

    public BrainsComponents(When when, GetInput getInput, Score score)
    {
      When = when;
      GetInput = getInput;
      Score = score;
    }
  }
}
