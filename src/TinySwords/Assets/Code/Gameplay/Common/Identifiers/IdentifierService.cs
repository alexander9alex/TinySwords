namespace Code.Gameplay.Common.Identifiers
{
  public class IdentifierService : IIdentifierService
  {
    private int _lastId = 1;

    public int Next() =>
      ++_lastId;
  }
}
