namespace Code.Gameplay.Features.Command.Services
{
  public interface ISelectableCommandService
  {
    bool IsCommandCompleted(GameEntity selectable);
    void RemoveCommand(GameEntity selectable);
  }
}
